using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        string phone=Console.ReadLine();
        string regex=@"^(\+91)?[6-9][0-9]{9}$";
        bool valid=Regex.IsMatch(phone,regex);
        Console.WriteLine(valid);
    }
}