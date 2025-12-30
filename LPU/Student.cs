using System;
namespace LPU
{
    class Student
    {
        public string Sname="";
        public int RegNo=0;
        public string Dept="";
        public string HostelName="";

        public void DisplayStudent()
        {
            Console.WriteLine("Student Name: "+Sname);
            Console.WriteLine("Registration Number: "+RegNo);
            Console.WriteLine("Department: "+Dept);
            Console.WriteLine("Hostel :"+HostelName);
        }
    }
}