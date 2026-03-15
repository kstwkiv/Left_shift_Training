using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, string> dialCodes = new Dictionary<string, string>
        {
            { "India", "+91" },
            { "USA", "+1" },
            { "Japan", "+81" }
        };

        string country = Console.ReadLine();
        // TODO: Use TryGetValue to print code or "Unavailable"
        if(dialCodes.TryGetValue(country,out string code))
        {
            Console.WriteLine(code);
        }
        else
        {
            Console.WriteLine("Unavailable");
        }
    }
}
