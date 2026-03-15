using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        string password=Console.ReadLine();
        string regex=@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[a-zA-Z0-9]).{8,}$";
        bool res=Regex.IsMatch(password,regex);
        Console.WriteLine(res);
    }
}