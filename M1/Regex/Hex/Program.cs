using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        string color=Console.ReadLine();
        string regex=@"^#([a-fA-F0-9]{3}|[a-fA-F0-9]{6})$";
        bool valid=Regex.IsMatch(color,regex);
        Console.WriteLine(valid);
    }
}