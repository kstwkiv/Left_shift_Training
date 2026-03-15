using System;
using System.Text;
class Program
{
   static string reversed(string input)
    {
         StringBuilder sb=new StringBuilder(input.Length);
        for(int i = input.Length - 1; i >= 0; i--)
        {
            sb.Append(input[i]);
        }
        return sb.ToString();
    }

    static void Main(string[] args)
    {
        string input=Console.ReadLine();
        //string rev=reversed(input);
        Console.WriteLine(reversed(input));
    }
}