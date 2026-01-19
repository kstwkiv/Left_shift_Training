using System;
namespace TopBrainsAssignments
{
    class Program
    {
        static void Main()
        {
            string s1=Console.ReadLine()??"";
            string s2=Console.ReadLine()??"";
            string output=AandV.Check(s1,s2);
            Console.WriteLine(output);
        }
    }
}