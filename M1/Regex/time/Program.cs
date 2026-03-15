using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        string time=Console.ReadLine();
        string regex=@"^([0-1][0-9]|2[0-3]):[0-5][0-9]$";
        bool valid=Regex.IsMatch(time,regex);
        Console.WriteLine(valid);
    }
}