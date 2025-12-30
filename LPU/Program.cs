using System;
namespace LPU
{
    class Program
    {
        public static void Main(String[] args)
        {
            Hostels h1=new Hostels();
            Console.WriteLine("Enter hostel name");
            h1.HostelName=Console.ReadLine();
            Console.WriteLine("Enetr Room No");
            h1.RoomNo=int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Bed Placement");
            h1.Bed=Console.ReadLine();

            Console.Write("------Student Hostel details--------");
            Console.Write("\n=====================================");
            Console.WriteLine("\n");
            h1.DisplayHostel();

            Student s1=new Student();
            Console.WriteLine("Enter your name");
            s1.Sname=Console.ReadLine();
            Console.WriteLine("Enter your registration number");
            s1.RegNo=int.Parse(Console.ReadLine());
            Console.WriteLine("Enter your department");
            s1.Dept=Console.ReadLine();
            s1.HostelName=h1.HostelName;

            Console.Write("------Student details--------");
            Console.Write("\n=====================================");
            Console.WriteLine("\n");
            s1.DisplayStudent();
        }
    }
}