using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Salary {get; set;}
}

class Program
{
    static void Main()
    {
       
        List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 101, Name = "A" },
            new Employee { Id = 105, Name = "B" },
            new Employee { Id = 103, Name = "C" },
            new Employee { Id = 105, Name = "D" },
            new Employee { Id = 102, Name = "E" }
        };
        int fmax=int.MinValue;
        int smax=int.MinValue;
        foreach(var emp in employees)
        {
            if (emp.Id > fmax)
            {
                smax=fmax;
                fmax=emp.Id;
            }
            else if(emp.Id<fmax && emp.Id>smax)
            {
                smax=emp.Id;
            }
        }
        
        Console.WriteLine("Second Highest " + smax);
    }
}
