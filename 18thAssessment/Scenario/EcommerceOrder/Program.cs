using System;
using System.Security.Cryptography.X509Certificates;
public class OutOfStockException : Exception
{
    public OutOfStockException(string Message) : base(Message)
    {
        
    }
}
public class OrderAlreadyShippedException : Exception
{
    public OrderAlreadyShippedException(string Message) : base(Message)
    {
        
    }
}
public class CustomerBlacklistedException : Exception
{
    public CustomerBlacklistedException(string Message) : base(Message)
    {
    }
}
public class Product
{
    public int Id{get;set;}
    public string Name{get;set;}
    public double Price{get;set;}
    public int Stock{get;set;}
    public Product(int id ,string name,double price,int stock)
    {
        Id=id;
        Name=name;
        Price=price;
        Stock=stock;
    }

}
public class Customer
{
    public int Id{get;set;}
    public string Name{get;set;}
    public bool IsBlackListed{get;set;}
    public Customer(int id,string name,bool blacklisted = false)
    {
     Id=id;
     Name=name;
     IsBlackListed=blacklisted;   
    }
}
public class OrderItem
{
    public Product Product{get;set;}
    public int Quantity{get;set;}
    public OrderItem(Product product,int quantity)
    {
        
        if (quantity > product.Stock)
        {
            throw new OutOfStockException($"stock not sufficient");
        }

        Product=product;
        Quantity=quantity;
        product.Stock-=quantity;

    }
    public double TotalPrice()
    {
        return Product.Price*Quantity;
    }
}
public class Order
{
    public int OrderId{get;set;}
    public Customer Customer{get;set;}
    public List<OrderItem> Items{get;set;}=new List<OrderItem>();
    public DateTime OrderDate{get;set;}
    public string OrderStatus{get;set;}
    public Order(int id,Customer customer)
    {
        if (customer.IsBlackListed)
        {
            throw new CustomerBlacklistedException("Customer is BlackListed");
        }

        OrderId=id;
        Customer=customer;
        OrderDate=DateTime.Now;
        OrderStatus="Created";
    }
    public void AddItem(Product product,    int quantity)
    {
        Items.Add(new OrderItem(product,quantity));
    }
    public double GetTotalAmount()
    {
        return Items.Sum(l=>l.TotalPrice());
    }
    public void ship()
    {
        OrderStatus="shipped";
    }
    public void Cancel()
    {
        if (OrderStatus == "shipped")
        {
            throw new OrderAlreadyShippedException("Order already shipped ");
        }
        OrderStatus="Cancelled";
    }
}
class Program
{
    static void Main()
    {
        List<Product>products=new List<Product>();
        List<Customer> customers=new List<Customer>();
        List<Order> orders=new List<Order>();
        Dictionary<int,Product> productDictionary=new Dictionary<int, Product>();
        products.Add(new Product(1, "Laptop", 60000, 20));
        products.Add(new Product(2, "Phone", 30000, 5));
        products.Add(new Product(3, "Headphones", 2000, 50));
        foreach (var p in products)
        {
            productDictionary[p.Id]=p;
        }
        customers.Add(new Customer(1, "Rahul"));
        customers.Add(new Customer(2, "Riya"));
        customers.Add(new Customer(3, "Amit", true));
        var order1=new Order(101,customers[0]);
        order1.AddItem(productDictionary[1],1);
        orders.Add(order1);
        var order2=new Order(102,customers[1]);
        order2.AddItem(productDictionary[2],2);
        orders.Add(order2);

        //orders in last 7 days;
        Console.WriteLine("Orders in last 7 days");
        var recentOrders=orders.Where(o=>o.OrderDate>=DateTime.Now.AddDays(-7));
        foreach(var r in recentOrders)
        {
            Console.WriteLine(r.OrderId);
        }
    }
}