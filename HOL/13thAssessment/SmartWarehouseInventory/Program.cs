using System;
using System.Collections.Generic;
public class DuplicateSKUException : InventoryException
{
    public DuplicateSKUException(string Message):base(Message){

    }
}
public class InventoryException : Exception
{
    public InventoryException(string Message):base(Message){

    }
}
public class LowStockException : InventoryException
{
    public LowStockException(string Message):base(Message){

    }
}
public class InvalidProductException : InventoryException
{
    public InvalidProductException(string Message):base(Message){

    }
}
public abstract class Product
{
    public string Name{get;set;}
    public int Priority{get;set;}
    public string SKU{get;set;}
    private int stock;
    private int threshold;
    public int Stock
    {
        get{return stock;}
        set
        {
            if (value < threshold)
            {
                throw new LowStockException($"Low stock below threshold for {Name}");
            }

            stock=value;
        }
    }
    protected Product(string sku,string name, int priority,int stock,int threshold)
    {
        if (priority < 1 || priority > 10)
        {
            throw new InvalidProductException("Product Priority must be between 1 to 10");
        }
        SKU=sku;
        Name=name;
        Priority=priority;
        this.threshold=threshold;
        Stock=stock;
    }
}
public class Electronics : Product
{
    public Electronics(string sku,string name,int priority,int stock):base(sku,name,priority,stock,5)
    {

    }
}
public class Perishable : Product
{
    public Perishable(string sku,string name,int priority,int stock):base(sku,name,priority,stock,10)
    {
        
    }
}
public class FragileItem : Product
{
    public FragileItem(string sku,string name,int priority,int stock) : base(sku, name, priority, stock, 3)
    {
        
    }
}
public class WareHouse
{
    private SortedDictionary<int , List<Product>>inventory=new SortedDictionary<int, List<Product>>();
    private HashSet<string> skuTracker=new HashSet<string>();
    public void AddProduct(Product product)
    {
        if (skuTracker.Contains(product.SKU))
        {
            throw new DuplicateSKUException($"{product.SKU} already exists. Cant add duplicated product.");
        }

        if (!inventory.ContainsKey(product.Priority))
        {
            inventory[product.Priority]=new List<Product>();
        }

        inventory[product.Priority].Add(product);
        skuTracker.Add(product.SKU);
    }
    public void RemoveProduct(string sku)
    {
        foreach(var pair in inventory)
        {
            var product= pair.Value.Find(p => p.SKU == sku);
                if (product!= null)
                {
                    pair.Value.Remove(product);
                    skuTracker.Remove(sku);
                    return;
                }
        }
        throw new InvalidProductException("Product Not Found");
    }
    public void UpdateStock(string sku,int newstock)
    {
        foreach(var pair in inventory)
        {
            var product=pair.Value.Find(p=>p.SKU==sku);
            if (product != null)
            {
                product.Stock=newstock;
                return;
            }
        }

        throw new InvalidProductException("Product not found");
    }

    public List<Product> getHighestProduct()
    {
        foreach(var pair in inventory)
        {
            return pair.Value;
        }
        return new List<Product>();
    }
}
class Program
{
    static void Main(string[] args)
    {
        try
        {
            WareHouse warehouse=new WareHouse();
            Product e1 = new Electronics("E101", "Laptop", 1, 10);
            Product p1 = new Perishable("P201", "Milk", 2, 20);
            Product f1 = new FragileItem("F301", "Glass Vase", 3, 5);
            warehouse.AddProduct(e1);
            warehouse.AddProduct(p1);
            warehouse.AddProduct(f1);
            Console.WriteLine("products added");

            warehouse.UpdateStock("E101",5);
            Console.WriteLine("Stock Updated for E101\n");

            Console.WriteLine("=======Highest Priority Products========");
            var topProducts=warehouse.getHighestProduct();
            foreach(var pro in topProducts)
            {
                Console.WriteLine($"Name: {pro.Name},SKU: {pro.SKU},Stock: {pro.Stock}");

            }
            Console.WriteLine("------------------------------");
            warehouse.RemoveProduct("P201");
            Console.WriteLine("Product P201 removed Successfully");

            warehouse.UpdateStock("F301",1);
        }
        catch(InventoryException e)
        {
            Console.WriteLine($"Inventory Error : {e.Message}");
        }
        catch(Exception e)
        {
            Console.WriteLine($"Exception : {e.Message}");
        }
    }
}