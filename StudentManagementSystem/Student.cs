using System;
using System.Runtime.InteropServices.Marshalling;
namespace StudentManagementSystem
{
    public enum CourseType
    {
        Civil,
        CSE,
        Mechanical,
        IT,
        Electrical
    }
    public class Student
    {
        public string Name{get;set;}
        public CourseType Course{get;set;} //enum type  
        public int Id{get;set;}
        public string Address{get;set;}
        public void Display()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Course: {Course}");
            Console.WriteLine($"Address: {Address}");
            Console.WriteLine("---------------------------------");
        }
    }
    public class StudentManager
    {
        private List<Student> students=new List<Student>();
        public void AddStudent(Student student)
        {
            students.Add(student);
            Console.WriteLine("Student added successfully.");
        }

        public void ViewAllStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students available");
                return;
            }

            foreach (student s in students)
            {
                Console.WriteLine($"Student Name is: {s.Name}");
                Console.WriteLine($"Student id: {s.Id}");
                Console.WriteLine($"Student course: {s.Course}");       
            }
        }
    }

}