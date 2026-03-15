using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        string employee=Console.ReadLine();
        string pattern=@"^EMP-20(0[0-9]|1[0-9]|2[0-5])-[0-9]{4}$";
        bool ans=Regex.IsMatch(employee,pattern);
        Console.WriteLine(ans);
    }
}