using System;
using System.Collections.Generic;
using System.Linq;
public class InvalidGPAException: Exception
{
    public InvalidGPAException(string Message) : base(Message)
    {
        
    }
}
public class DuplicateStudentException : Exception
{
    public DuplicateStudentException(string Message) : base(Message)
    {
        
    }
}
public class StudentNotFoundException : Exception
{
    public StudentNotFoundException(string Message) : base(Message)
    {
        
    }
}
public class Student
{
    public string Id{get;set;}
    public string Name{get;set;}
    public double GPA{get;set;}
    public Student(string id,string name,double gpa)
    {
        Id=id;
        Name=name;
        GPA=gpa;
    }
}
public class StudentUtility
{
    private SortedDictionary<double,List<Student>> students=new SortedDictionary<double, List<Student>>();
    public void Display()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students available");
            return;
        }

        var gpas=students.Keys.Reverse(); //keys are property not a method
        int rank=1;
        foreach(var gpa in gpas)
        {
            foreach(var student in students[gpa])
            {
                Console.WriteLine($"{rank} Rank - {student.Name} {student.Id} GPA: {student.GPA}");
                rank++;
            }
        }
        
    }
    public void UpdateGPA(string id,double newGPA)
    {
        if (newGPA < 0 || newGPA>10)
        {
            throw new InvalidGPAException($"GPA must be greater than 0 and lower than 10");
        }

        foreach(var gpa in students.Keys.ToList())
        {
            foreach(var stu in students[gpa])
            {
                if (stu.Id == id)
                {
                    students[gpa].Remove(stu);
                    if (students[gpa].Count == 0)
                    {
                        students.Remove(gpa);
                    }

                    stu.GPA=newGPA;
                    if (!students.ContainsKey(newGPA))
                    {
                        students[newGPA]=new List<Student>();
                    }

                    students[newGPA].Add(stu);
                    return ;
                }
            }
        }
        throw new StudentNotFoundException($"Student was not found");

    }
    public void AddStudent(Student student)
    {
        if(student.GPA>10 || student.GPA < 0)
        {
            throw new InvalidGPAException($"GPA must be in between 0 and 10");
        }

        foreach(var stu in students.Values)
        {
            foreach(var det in stu)
            {
                if (det.Id == student.Id)
                {
                    throw new DuplicateStudentException($"Student already exixts");
                }
            }
        }

        if (!students.ContainsKey(student.GPA))
        {
            students[student.GPA]=new List<Student>();
        }

        students[student.GPA].Add(student);
    }
}
class Program
{
    static void Main(string[] args)
    {
        StudentUtility util =new StudentUtility();
        while (true)
        {
            Console.WriteLine("1 Display Ranking");
            Console.WriteLine("2. Update GPA");
            Console.WriteLine("3 Add Student");
            Console.WriteLine("4 Exit");
            int choice =int.Parse(Console.ReadLine());
            try
            {
                switch (choice)
                {
                    case 1:
                    util.Display();
                    break;
                    case 2:
                    Console.WriteLine("Enter ID: ");
                    string id=Console.ReadLine();
                    Console.WriteLine("Enter new GPA: ");
                    double newGPA=double.Parse(Console.ReadLine());

                    util.UpdateGPA(id,newGPA);
                    Console.WriteLine("GPA updated");
                    break;
                    case 3:
                    Console.WriteLine("Enter id name gpa");
                    string[] input=Console.ReadLine()!.Split(' ');
                    string newid=input[0];
                    string name=input[1];
                    double gpa=double.Parse(input[2]);
                    Student stu=new Student(newid,name,gpa);
                    util.AddStudent(stu);
                    break;
                    case 4:
                    Console.WriteLine("Exit");
                    return;
                    default:
                    Console.WriteLine("Invalid Choice");
                    break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}