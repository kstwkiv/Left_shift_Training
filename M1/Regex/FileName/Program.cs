using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        string filename=Console.ReadLine();
        string regex=@"^[a-zA-Z0-9_]+\.(png|pdf|zip)$";
        bool valid=Regex.IsMatch(filename,regex);
        Console.WriteLine(valid);
    }
}