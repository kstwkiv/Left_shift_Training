using System;
namespace TopBrainsAssignments
{
    class Program
    {
        static void Main()
        {
            /*string s1=Console.ReadLine()??"";
            string s2=Console.ReadLine()??"";
            string output=AandV.Check(s1,s2);
            Console.WriteLine(output);*/

            int a=10,b=20;
            Swap.SwapRef(ref a, ref b);
            Console.WriteLine("values after swapping with reference: a= "+ a+" b= "+b);
            int x,y;
            Swap.SwapOut(a,b,out x,out y);
            Console.WriteLine("values after swapping with Out: a= "+ x+" b= "+y);

        }
    }
}