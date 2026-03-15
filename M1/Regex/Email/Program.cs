using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        string email=Console.ReadLine();
        string regex=@"[a-z]\.(cse|ece|mech|civil)\.[0-9]{4}\@lpu\.edu$";
        bool res=Regex.IsMatch(email,regex);
        Console.WriteLine(res);
    }
}