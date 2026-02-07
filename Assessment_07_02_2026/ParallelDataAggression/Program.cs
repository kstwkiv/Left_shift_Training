using System;
using System.Linq;
using System.Collections.Generic;

public class Sale
{
    public string Region { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}

public static class SalesAnalyzer
{
    public static List<(string Region, decimal TotalSales)> TotalSalesByRegion(List<Sale> sales)
    {
        return sales
            .GroupBy(s => s.Region)
            .Select(g => (Region: g.Key, TotalSales: g.Sum(x => x.Amount)))
            .OrderBy(x => x.Region)
            .ToList();
    }

    public static List<(string Region, string Category, decimal Total)> TopCategoryPerRegion(List<Sale> sales)
    {
        return sales
            .GroupBy(s => s.Region)
            .Select(regionGroup =>
            {
                var topCategory = regionGroup
                    .GroupBy(x => x.Category)
                    .Select(cat => new
                    {
                        Category = cat.Key,
                        Total = cat.Sum(x => x.Amount)
                    })
                    .OrderByDescending(x => x.Total)
                    .ThenBy(x => x.Category)
                    .First();

                return (Region: regionGroup.Key,
                        Category: topCategory.Category,
                        Total: topCategory.Total);
            })
            .OrderBy(x => x.Region)
            .ToList();
    }

    public static (DateTime Date, decimal Total) BestSalesDay(List<Sale> sales)
    {
        var result = sales
            .GroupBy(s => s.Date.Date)
            .Select(g => new
            {
                Date = g.Key,
                Total = g.Sum(x => x.Amount)
            })
            .OrderByDescending(x => x.Total)
            .ThenBy(x => x.Date)
            .First();

        return (result.Date, result.Total);
    }
}

class Program
{
    static void Main()
    {
        List<Sale> sales = new List<Sale>
        {
            new Sale { Region="North", Category="Electronics", Amount=5000, Date=new DateTime(2026,1,1)},
            new Sale { Region="North", Category="Clothing", Amount=2000, Date=new DateTime(2026,1,1)},
            new Sale { Region="South", Category="Electronics", Amount=7000, Date=new DateTime(2026,1,2)},
            new Sale { Region="South", Category="Clothing", Amount=3000, Date=new DateTime(2026,1,2)},
            new Sale { Region="North", Category="Electronics", Amount=4000, Date=new DateTime(2026,1,3)},
            new Sale { Region="East", Category="Furniture", Amount=8000, Date=new DateTime(2026,1,2)}
        };

        var totalRegion = SalesAnalyzer.TotalSalesByRegion(sales);
        var topCategory = SalesAnalyzer.TopCategoryPerRegion(sales);
        var bestDay = SalesAnalyzer.BestSalesDay(sales);

        Console.WriteLine("Total Sales By Region");
        foreach (var r in totalRegion)
            Console.WriteLine($"{r.Region} -> {r.TotalSales}");

        Console.WriteLine("\nTop Category Per Region");
        foreach (var r in topCategory)
            Console.WriteLine($"{r.Region} -> {r.Category} : {r.Total}");

        Console.WriteLine("\nBest Sales Day");
        Console.WriteLine($"{bestDay.Date.ToShortDateString()} -> {bestDay.Total}");
    }
}
