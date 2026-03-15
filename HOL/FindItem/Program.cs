using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static SortedDictionary<string, long> itemDetails =
        new SortedDictionary<string, long>()
        {
            { "Pen", 120 },
            { "Book", 300 },
            { "Pencil", 80 },
            { "Eraser", 80 }
        };

    public static SortedDictionary<string, long> FindItemDetails(long soldCount)
    {
        SortedDictionary<string, long> result =
            new SortedDictionary<string, long>();

        foreach (var item in itemDetails)
        {
            if (item.Value == soldCount)
            {
                result.Add(item.Key, item.Value);
            }
        }

        return result;
    }

    public static List<string> FindMinAndMaxSoldItems()
    {
        long min = itemDetails.Values.Min();
        long max = itemDetails.Values.Max();

        string minItem = itemDetails.First(x => x.Value == min).Key;
        string maxItem = itemDetails.First(x => x.Value == max).Key;

        return new List<string> { minItem, maxItem };
    }

    public static Dictionary<string, long> SortByCount()
    {
        return itemDetails
            .OrderBy(x => x.Value)
            .ToDictionary(x => x.Key, x => x.Value);
    }

    static void Main()
    {
        Console.WriteLine("Enter sold count:");
        long soldCount = long.Parse(Console.ReadLine());

        var foundItems = FindItemDetails(soldCount);

        if (foundItems.Count == 0)
        {
            Console.WriteLine("Invalid sold count");
        }
        else
        {
            foreach (var item in foundItems)
            {
                Console.WriteLine(item.Key + " : " + item.Value);
            }
        }

        var minMaxItems = FindMinAndMaxSoldItems();
        Console.WriteLine("Minimum Sold Item: " + minMaxItems[0]);
        Console.WriteLine("Maximum Sold Item: " + minMaxItems[1]);

        var sortedItems = SortByCount();
        Console.WriteLine("Items sorted by sold count:");
        foreach (var item in sortedItems)
        {
            Console.WriteLine(item.Key + " : " + item.Value);
        }
    }
}
