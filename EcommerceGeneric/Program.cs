using System;
using System.Collections.Generic;
using System.Linq;
public interface IProduct
{
    int Id { get; }
    string Name { get; }
    decimal Price { get; }
    Category Category { get; }
}

public enum Category { Electronics, Clothing, Books, Groceries }

// 1. Create a generic repository for products
public class ProductRepository<T> where T : class, IProduct
{
    private List<T> _products = new List<T>();
    
    // TODO: Implement method to add product with validation
    public void AddProduct(T product)
    {
        if (product == null)
        {
            throw new ArgumentNullException("Product Cannot be Null");
        }

        if (_products.Any(p => p.Id == product.Id))
        {
            throw new Exception("No duplicate products are allowed");
        }

        if (product.Price > 0)
        {
            throw new Exception("Price cannot be negative");
        }

        if (product.Name == null)
        {
            throw new Exception("Name cannot be null");
        }

        _products.Add(product);

        // Rule: Product ID must be unique
        // Rule: Price must be positive
        // Rule: Name cannot be null or empty
        // Add to collection if validation passes
    }
    
    // TODO: Create method to find products by predicate
    public IEnumerable<T> FindProducts(Func<T, bool> predicate)
    {
        // Should return filtered products
        return _products.Where(predicate);
    }
    
    // TODO: Calculate total inventory value
    public decimal CalculateTotalValue()
    {
        // Return sum of all product prices
        return _products.Sum(p=>p.Price);
    }
}

// 2. Specialized electronic product
public class ElectronicProduct : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Category Category => Category.Electronics;
    public int WarrantyMonths { get; set; }
    public string Brand { get; set; }
}

// 3. Create a discounted product wrapper
public class DiscountedProduct<T> where T : IProduct
{
    private T _product;
    private decimal _discountPercentage;
    
    public DiscountedProduct(T product, decimal discountPercentage)
    {
        if (product == null)
        {
            throw new ArgumentNullException("product cannot be null");
        }

        if (discountPercentage < 0 || discountPercentage > 100)
        {
            throw new Exception("discount percentage must be between 0 and 100");
        }

        _product=product;
        _discountPercentage=discountPercentage;
        // TODO: Initialize with validation
        // Discount must be between 0 and 100

    }
    
    // TODO: Implement calculated price with discount
    public decimal DiscountedPrice => _product.Price * (1 - _discountPercentage / 100);

    // TODO: Override ToString to show discount details

    public override string ToString()
    {
        return $"original price : {_product.Price}"+$"final price : {DiscountedPrice}";
    }
}

// 4. Inventory manager with constraints
public class InventoryManager
{
    // TODO: Create method that accepts any IProduct collection
    public void ProcessProducts<T>(IEnumerable<T> products) where T : IProduct
    {
        // a) Print all product names and prices
        // b) Find the most expensive product
        // c) Group products by category
        // d) Apply 10% discount to Electronics over $500
        Console.WriteLine("\n------List of products-----");
        foreach(var p in products)
        {
            Console.WriteLine($"{p.Name} -{p.Price}");
        }
        Console.WriteLine();
        var expensive=products.OrderByDescending(p=>p.Price).FirstOrDefault();
        Console.WriteLine($"Most expensive product: {expensive}");
        Console.WriteLine();
        Console.WriteLine("\n----grouped by category-----");
        var grouped=products.GroupBy(p=>p.Category);
        foreach(var p in grouped)
        {
            Console.WriteLine(p.key);
            foreach(var k in p)
            {
                Console.WriteLine($"{k.Name}");
            }
        }
    }
    
    // TODO: Implement bulk price update with delegate
    public void UpdatePrices<T>(List<T> products, Func<T, decimal> priceAdjuster) 
        where T : IProduct
    {
        // Apply priceAdjuster to each product
        // Handle exceptions gracefully
    }
}

// 5. TEST SCENARIO: Your tasks:
// a) Implement all TODO methods with proper error handling
// b) Create a sample inventory with at least 5 products
// c) Demonstrate:
//    - Adding products with validation
//    - Finding products by brand (for electronics)
//    - Applying discounts
//    - Calculating total value before/after discount
//    - Handling a mixed collection of different product types
public class Solution
{
    public static void Main()
    {
        var repo = new ProductRepository<IProduct>();

        var p1 = new ElectronicProduct
        {
            Id = 1,
            Name = "Laptop",
            Price = 60000,
            WarrantyMonths = 24,
            Brand = "Dell"
        };

        var p2 = new ElectronicProduct
        {
            Id = 2,
            Name = "Mobile",
            Price = 30000,
            WarrantyMonths = 12,
            Brand = "Samsung"
        };

        var p3 = new GeneralProduct
        {
            Id = 3,
            Name = "Shirt",
            Price = 1500,
            Category = Category.Clothing
        };

        var p4 = new GeneralProduct
        {
            Id = 4,
            Name = "Novel",
            Price = 500,
            Category = Category.Books
        };

        var p5 = new GeneralProduct
        {
            Id = 5,
            Name = "Rice Bag",
            Price = 2000,
            Category = Category.Groceries
        };

        // Adding Products
        repo.AddProduct(p1);
        repo.AddProduct(p2);
        repo.AddProduct(p3);
        repo.AddProduct(p4);
        repo.AddProduct(p5);


        // Find Electronics by Brand
        Console.WriteLine("\n---- Find Dell Products ----");
        var dellProducts = repo.FindProducts(p =>
            p is ElectronicProduct e && e.Brand == "Dell");

        foreach (var p in dellProducts)
            Console.WriteLine(p.Name);


        // Total Inventory Value
        Console.WriteLine($"\nTotal Inventory Value = {repo.CalculateTotalValue()}");


        // Discount Demo
        Console.WriteLine("\n---- Discount Demo ----");
        var discount = new DiscountedProduct<IProduct>(p1, 15);
        Console.WriteLine(discount);


        // Inventory Manager Processing
        InventoryManager manager = new InventoryManager();
        var allProducts = repo.GetAll();

        manager.ProcessProducts(allProducts);


        // Bulk Price Update
        Console.WriteLine("\n---- Bulk Price Update (+5%) ----");
        manager.UpdatePrices(allProducts, p => p.Price * 1.05m);
    }
}