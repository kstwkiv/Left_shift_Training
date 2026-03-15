using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        string name=Console.ReadLine();
        string regex=@"^([a-z])[a-z]\1$";
        bool valid=Regex.IsMatch(name,regex);
        Console.WriteLine(valid);
    }
}