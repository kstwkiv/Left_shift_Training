using System;
namespace CopyConst
{
    class CoCo
    {
        int x;
        public CoCo(int i) //parameterized constructor
        {
            x=i;
        }
        public CoCo(CoCo obj) //copy constructor
        {
            x=obj.x;
        }
        public void Display()
        {
            Console.WriteLine("Copy Constructor is called for :"+x);
        }
    }

    class Program
    {
        static void Main()
        {
            CoCo coConst=new CoCo(12);
            coConst.Display();
            CoCo coConst2=new CoCo(coConst);
            coConst2.Display();
        }
    }
}