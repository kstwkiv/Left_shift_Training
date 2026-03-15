using System;
using System.Collections.Generic;
public class CakeOrder
{
    Dictionary<string,double> orders=new Dictionary<string, double>();
    public void AddOrder()
    {
        Console.WriteLine("Enter number of cake orders to be added: ");
        int n=int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the cake order details (Order Id: Cake Cost)");
        for(int i = 0; i < n; i++)
        {
            string OrderId=Console.ReadLine();
            double CakeCost=double.Parse(Console.ReadLine());
            orders.Add(OrderId,CakeCost);
        }

        Console.WriteLine("List of all cake orders with their costs!");
        foreach(var item in orders)
        {
            Console.WriteLine(item.Key+" : "+item.Value);
        }
    }

    public void findOrdersAbove()
    {
        Console.WriteLine("enter the budget: ");
        double budget=double.Parse(Console.ReadLine());
        Console.WriteLine("Cakes under budget ->"+budget);
        foreach(var ord in orders)
        {
            if (ord.Value >= budget)
            {
                Console.WriteLine(ord.Key+" : "+ord.Value);
            }
        }
    }

}

public class Program
{
    public static void Main(String[] args)
    {
        CakeOrder cake = new CakeOrder();
        cake.AddOrder();
        cake.findOrdersAbove();

    }
}