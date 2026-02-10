using LINQ_Assignment_BoilerPlateCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Assignment_BoilerPlateCode.Repos
{
   public class EmployeeRepo
    {
        public static List<Employee> SeedEmployees()
        {
     
            return new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Amit Sharma", Department = "IT", Salary = 85000, Skills =  new List<String>() { "C#", "SQL", "LINQ" } },
                new Employee() { Id = 2, Name = "Priya Verma", Department = "HR", Salary = 60000, Skills = new List<String>() { "Recruitment", "Communication" } },
                new Employee { Id = 3, Name = "Rahul Mehta", Department = "Finance", Salary = 95000, Skills = new List<String>() { "Excel", "Accounting" } },
                new Employee { Id = 4, Name = "Sneha Iyer", Department = "IT", Salary = 78000, Skills = new List<String>() { "JavaScript", "React", "C#" } },
                new Employee { Id = 5, Name = "Alok Gupta", Department = "IT", Salary = 132000, Skills = new List<String>() { "C#", "Azure", "Microservices" } },
                new Employee { Id = 5, Name = "Riya Jain", Department = "IT", Salary = 92000, Skills = new List<String>() { "C#", "Azure", "Microservices" } },
                new Employee { Id = 6, Name = "Anita Desai", Department = "Marketing", Salary = 58000, Skills = new List<String>() { "SEO", "Content Marketing" } }

            };
        }

        
    }
}
