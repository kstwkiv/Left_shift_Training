using System;
namespace LPU
{
    class Student
    {
        public string Sname{get;set;}
        public int RegNo{get;set;}
        public string Dept{get;set;}
        public string HostelName{get;set;}

        public void DisplayStudent()
        {
            Console.WriteLine("Student Name: "+Sname);
            Console.WriteLine("Registration Number: "+RegNo);
            Console.WriteLine("Department: "+Dept);
            Console.WriteLine("Hostel :"+HostelName);
        }
    }
}