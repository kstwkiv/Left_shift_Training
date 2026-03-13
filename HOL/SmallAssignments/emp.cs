using System;

class Employee
{
    public int Id;
    public string Name;
    public double Salary;

    public Employee(int id, string name, double salary)
    {
        Id = id;
        Name = name;
        Salary = salary;
    }

    public Employee(Employee emp)
    {
        Id = emp.Id;
        Name = emp.Name;
        Salary = emp.Salary;
    }

    public void Display()
    {
        Console.WriteLine($"Id: {Id}, Name: {Name}, Salary: {Salary}");
    }
}