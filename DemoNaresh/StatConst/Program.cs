using System;
namespace StatConst
{
    class SC
    {
        static SC()
        {
            Console.WriteLine("Static Constructor is called.");
        }
    }
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Main Method is called.");
            SC sc=new SC();
        }
    }
}