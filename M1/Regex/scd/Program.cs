using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        string text="Hello123";
        bool res=Regex.IsMatch(text,@"\d");
        Console.WriteLine(res);
    }
}