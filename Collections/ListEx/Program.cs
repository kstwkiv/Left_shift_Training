using System;
using System.Collections.Generic;
class Program
{
    static void Main(string[] args)
    {
        List<int>nums=new List<int>();
        int n=int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the values");
       for(int i = 0; i < n; i++)
        {
            int num =int.Parse(Console.ReadLine());
            nums.Add(num);
        }
        foreach(int num in nums)
        {
            Console.WriteLine(num);
        }
        nums.Remove(23);
        foreach(int num in nums)
        {
            Console.WriteLine(num);
        }
    }
}