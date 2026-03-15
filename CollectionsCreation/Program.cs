using System;
using System.Collections.Generic;
using System.Globalization;
public class Program
{
    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        string[] vals = Console.ReadLine().Split(' ');
        List<int> nums = new List<int>();

        for (int i = 0; i < n; i++)
        {
            nums.Add(int.Parse(vals[i]));
        }

        Console.WriteLine(string.Join(", ", nums));



        int k=int.Parse(Console.ReadLine());
        var arr=new List<int>();
        for(int i=0;i<k;i++){
            arr.Add(int.Parse(Console.ReadLine()));
        }

        Console.WriteLine(arr);
    }

}