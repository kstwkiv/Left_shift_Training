using System;
namespace ParamConst
{
    
    class NewData
    {
        int k;
        public void display()
        {
        Console.WriteLine("X value is :"+k);
        }
        public NewData(int a)
        {
            Console.WriteLine("Parameterized constructor is called "+(a*a));
        }
    }
    class Program
    {
        
        static void Main()
        {
            NewData p=new NewData(12);
            NewData p1=new NewData(3);
            p.display();
        }
    }
}