using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        string text="sathwika82";
        bool res=Regex.IsMatch(text,@"^[a-zA-Z]+\d+$");
        Console.WriteLine(res);
        string email="sathwika2004@lpu.com";
        bool result=Regex.IsMatch(email,@"^[a-z]+[0-9]+@gmail\.com$");
        Console.WriteLine(result);
        string comp="sathwika.lpit.2004@IT.company.com";
        bool answer=Regex.IsMatch(comp,@"^[a-z]{3,}\.[a-z]{3,}\.\d{4}@(IT|HR|Admin)\.company\.com$");
        Console.WriteLine(answer);
        
    }
}