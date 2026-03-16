using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRS.Data;
using PRS.DTOs;
using PRS.Entities;

namespace PRS.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(AppDbContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ── GET api/v1/orders ─────────────────────────────────────────────────
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<OrderBasicDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResultDto<OrderBasicDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var total = await _context.Orders.CountAsync();

            var orders = await _context.Orders
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return Ok(new PagedResultDto<OrderBasicDto>
            {
                Items = orders.Select(o => new OrderBasicDto
                {
                    Id = o.Id,
                    OrderNumber = o.OrderNumber,
                    CustomerName = o.CustomerName,
                    Status = o.Status.ToString(),
                    TotalAmount = o.TotalAmount,
                    CreatedAt = o.CreatedAt,
                    ItemCount = o.Items.Count
                }).ToList(),
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize)
            });
        }

        // ── GET api/v1/orders/{orderNumber} ───────────────────────────────────
        [HttpGet("{orderNumber}")]
        [ProducesResponseType(typeof(OrderDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDetailDto>> GetByOrderNumber(string orderNumber)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

            if (order == null)
                return NotFound(new { message = $"Order '{orderNumber}' not found." });

            return Ok(MapToDetailDto(order));
        }

        // ── POST api/v1/orders ────────────────────────────────────────────────
        [HttpPost]
        [ProducesResponseType(typeof(OrderDetailDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailDto>> Create([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Validate all products exist and are available
            var orderItems = new List<OrderItem>();
            decimal subTotal = 0;

            foreach (var item in dto.Items)
            {
                var product = await _context.Products
                    .Include(p => p.Inventory)
                    .FirstOrDefaultAsync(p => p.Id == item.ProductId && p.IsActive);

                if (product == null)
                    return BadRequest(new { message = $"Product {item.ProductId} not found or inactive." });

                if (product.Inventory == null || product.Inventory.AvailableQuantity < item.Quantity)
                    return BadRequest(new { message = $"Insufficient stock for product '{product.Name}'. Available: {product.Inventory?.AvailableQuantity ?? 0}." });

                var unitPrice = product.DiscountedPrice ?? product.Price;
                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    SKU = product.SKU,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = unitPrice * item.Quantity
                });

                subTotal += unitPrice * item.Quantity;
            }

            // Build order
            var taxAmount = Math.Round(subTotal * 0.1m, 2);   // 10% tax
            var shippingAmount = subTotal >= 100 ? 0m : 9.99m;      // Free shipping over $100
            var totalAmount = subTotal + taxAmount + shippingAmount;

            var order = new Order
            {
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}",
                CustomerName = dto.CustomerName,
                CustomerEmail = dto.CustomerEmail,
                ShippingAddress = dto.ShippingAddress,
                Notes = dto.Notes,
                SubTotal = subTotal,
                TaxAmount = taxAmount,
                ShippingAmount = shippingAmount,
                TotalAmount = totalAmount,
                Status = OrderStatus.Pending,
                PaymentStatus = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                Items = orderItems
            };

            // Deduct inventory (wrapped in a transaction)
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                foreach (var item in dto.Items)
                {
                    var inv = await _context.Inventories
                        .FirstOrDefaultAsync(i => i.ProductId == item.ProductId);
                    if (inv != null)
                    {
                        inv.ReservedQuantity += item.Quantity;
                        inv.LastUpdatedAt = DateTime.UtcNow;

                        _context.InventoryTransactions.Add(new InventoryTransaction
                        {
                            InventoryId = inv.Id,
                            Type = TransactionType.Sale,
                            Quantity = item.Quantity,
                            Notes = $"Reserved for order {order.OrderNumber}",
                            CreatedBy = dto.CustomerEmail,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }

            _logger.LogInformation("Order created: {OrderNumber}", order.OrderNumber);

            return CreatedAtAction(nameof(GetByOrderNumber),
                new { orderNumber = order.OrderNumber }, MapToDetailDto(order));
        }

        // ── PATCH api/v1/orders/{orderNumber}/status ──────────────────────────
        [HttpPatch("{orderNumber}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(
            string orderNumber, [FromBody] UpdateOrderStatusDto dto)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

            if (order == null)
                return NotFound(new { message = $"Order '{orderNumber}' not found." });

            order.Status = dto.Status;
            order.UpdatedAt = DateTime.UtcNow;

            if (dto.Status == OrderStatus.Shipped) order.ShippedAt = DateTime.UtcNow;
            if (dto.Status == OrderStatus.Delivered) order.DeliveredAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Order status updated.", orderNumber, status = dto.Status.ToString() });
        }

        // ── HELPER ────────────────────────────────────────────────────────────
        private static OrderDetailDto MapToDetailDto(Order o) => new()
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            CustomerName = o.CustomerName,
            CustomerEmail = o.CustomerEmail,
            Status = o.Status.ToString(),
            PaymentStatus = o.PaymentStatus.ToString(),
            SubTotal = o.SubTotal,
            TaxAmount = o.TaxAmount,
            ShippingAmount = o.ShippingAmount,
            TotalAmount = o.TotalAmount,
            ShippingAddress = o.ShippingAddress,
            Notes = o.Notes,
            CreatedAt = o.CreatedAt,
            ShippedAt = o.ShippedAt,
            DeliveredAt = o.DeliveredAt,
            Items = o.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                SKU = i.SKU,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.TotalPrice
            }).ToList()
        };
    }
