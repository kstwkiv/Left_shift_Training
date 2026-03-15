using System;
namespace NewStudent
{
    class Student
    {
        string Name;
        public Student(string stuName)  //explicit constructor
        {
            Name=stuName;
            Console.WriteLine("Student name is : "+Name);
        }
    }
    class Program
    {
        static void Main()
        {
            Student st=new Student("Sathwika");
        }
    }
}