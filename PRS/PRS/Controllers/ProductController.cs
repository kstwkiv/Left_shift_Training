using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRS.API

using PRS.DTOs;

using PRS.Services.Interfaces;
namespace PRS.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _svc;

        public ProductsController(IProductService svc) => _svc = svc;

        // ── GET api/v1/products ───────────────────────────────────────────────
        /// <summary>Returns a paginated, filtered list of products.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<ProductSummaryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResultDto<ProductSummaryDto>>> GetAll(
            [FromQuery] ProductFilterDto filter,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
            => Ok(await _svc.GetAllProductsAsync(filter, pageNumber, pageSize));

        // ── GET api/v1/products/{id} ──────────────────────────────────────────
        /// <summary>Returns full detail for a single product.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDetailDto>> Get(int id)
        {
            try { return Ok(await _svc.GetProductByIdAsync(id)); }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        // ── POST api/v1/products ──────────────────────────────────────────────
        /// <summary>Creates a new product.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDetailDto>> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var product = await _svc.CreateProductAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
            catch (InvalidOperationException ex) { return BadRequest(new { message = ex.Message }); }
            catch (KeyNotFoundException ex) { return BadRequest(new { message = ex.Message }); }
        }

        // ── PUT api/v1/products/{id} ──────────────────────────────────────────
        /// <summary>Updates an existing product. Send only fields you want to change.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDetailDto>> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try { return Ok(await _svc.UpdateProductAsync(id, dto)); }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        // ── DELETE api/v1/products/{id} ───────────────────────────────────────
        /// <summary>Permanently deletes a product.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
            => await _svc.DeleteProductAsync(id) ? NoContent() : NotFound();

        // ── PATCH api/v1/products/{id}/soft-delete ────────────────────────────
        /// <summary>Marks a product as inactive without deleting it.</summary>
        [HttpPatch("{id:int}/soft-delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SoftDelete(int id)
            => await _svc.SoftDeleteProductAsync(id) ? NoContent() : NotFound();

        // ── GET api/v1/products/featured ──────────────────────────────────────
        /// <summary>Returns featured products.</summary>
        [HttpGet("featured")]
        [ProducesResponseType(typeof(List<ProductSummaryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductSummaryDto>>> GetFeatured(
            [FromQuery] int count = 10)
            => Ok(await _svc.GetFeaturedProductsAsync(count));

        // ── GET api/v1/products/best-selling ──────────────────────────────────
        /// <summary>Returns best-selling products.</summary>
        [HttpGet("best-selling")]
        [ProducesResponseType(typeof(List<ProductSummaryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductSummaryDto>>> GetBestSelling(
            [FromQuery] int count = 10)
            => Ok(await _svc.GetBestSellingProductsAsync(count));

        // ── GET api/v1/products/new-arrivals ──────────────────────────────────
        /// <summary>Returns recently added products.</summary>
        [HttpGet("new-arrivals")]
        [ProducesResponseType(typeof(List<ProductSummaryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductSummaryDto>>> GetNewArrivals(
            [FromQuery] int days = 30,
            [FromQuery] int count = 10)
            => Ok(await _svc.GetNewArrivalsAsync(days, count));

        // ── GET api/v1/products/{id}/related ─────────────────────────────────
        /// <summary>Returns products related to a given product (same category).</summary>
        [HttpGet("{id:int}/related")]
        [ProducesResponseType(typeof(List<ProductSummaryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ProductSummaryDto>>> GetRelated(int id,
            [FromQuery] int count = 5)
        {
            try { return Ok(await _svc.GetRelatedProductsAsync(id, count)); }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        // ── GET api/v1/products/{id}/inventory ────────────────────────────────
        /// <summary>Returns inventory status for a product.</summary>
        [HttpGet("{id:int}/inventory")]
        [ProducesResponseType(typeof(InventoryStatusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventoryStatusDto>> GetInventory(int id)
        {
            try { return Ok(await _svc.GetProductInventoryAsync(id)); }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        // ── PUT api/v1/products/{id}/inventory ────────────────────────────────
        /// <summary>Updates the stock quantity for a product.</summary>
        [HttpPut("{id:int}/inventory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateInventory(int id, [FromBody] UpdateInventoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return await _svc.UpdateInventoryAsync(id, dto) ? NoContent() : NotFound();
        }

        // ── GET api/v1/products/{id}/sku-check ───────────────────────────────
        /// <summary>Checks whether a SKU is available (not already in use).</summary>
        [HttpGet("sku-check")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckSku(
            [FromQuery] string sku,
            [FromQuery] int? excludeId = null)
        {
            var isUnique = await _svc.IsSkuUniqueAsync(sku, excludeId);
            return Ok(new { sku, isUnique });
        }
    }
}
