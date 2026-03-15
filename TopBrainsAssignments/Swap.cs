using System;
namespace TopBrainsAssignments{
    class Swap
    {
        public static void SwapRef(ref int a, ref int b)
        {
            a=a+b;
            b=a-b;
            a=a-b;
        }

        public static void SwapOut(int k, int t, out int x,out int y)
        {
            x=t;
            y=k;
        }
    }
}