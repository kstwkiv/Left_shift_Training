using System;
using System.Text.RegularExpressions;
using System.Net;
class Program
{
    static void Main()
    {
        string ip=Console.ReadLine();
        //bool valid =IPAddress.TryParse(ip,out _);
        string regex=@"((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.){3}(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])$";
        bool valid=Regex.IsMatch(ip,regex);
        Console.WriteLine(valid);
    }
}