using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQ_Assignment_BoilerPlateCode.Repos;
using LINQ_Assignment_BoilerPlateCode.DTOs;
using LINQ_Assignment_BoilerPlateCode.Models;

namespace LINQ_Assignment_BoilerPlateCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var employees = EmployeeRepo.SeedEmployees();
            var projects = ProjectRepo.SeedProjects();

            Console.WriteLine("🟢 LINQ Assignment - All Solutions ✅");
            Console.WriteLine($"Loaded {employees.Count} employees, {projects.Count} projects\n");

            // Run all tests
            TestAllSolutions(employees, projects);
        }

        // 🟢 SECTION 1 – HR ANALYTICS
        static List<Employee> GetHighEarningEmployees(List<Employee> employees)
        {
            return employees.Where(e => e.Salary > 60000).ToList();
        }

        static List<string> GetEmployeeNames(List<Employee> employees)
        {
            return employees.Select(e => e.Name).ToList();
        }

        static bool HasHREmployees(List<Employee> employees)
        {
            return employees.Any(e => e.Department == "HR");
        }

        // 🟡 SECTION 2 – MANAGEMENT INSIGHTS
        static List<DepartmentCount> GetDepartmentWiseCount(List<Employee> employees)
        {
            return employees.GroupBy(e => e.Department)
                           .Select(g => new DepartmentCount { Department = g.Key, Count = g.Count() })
                           .ToList();
        }

        static Employee GetHighestPaidEmployee(List<Employee> employees)
        {
            return employees.OrderByDescending(e => e.Salary).FirstOrDefault();
        }

        static List<Employee> SortEmployeesBySalaryAndName(List<Employee> employees)
        {
            return employees.OrderByDescending(e => e.Salary)
                           .ThenBy(e => e.Name)
                           .ToList();
        }

        // 🔵 SECTION 3 – PROJECT & SKILL INTELLIGENCE
        static List<EmployeeProject> GetEmployeeProjectMappings(List<Employee> employees, List<Project> projects)
        {
            return employees.Join(projects,e => e.Id,p => p.EmployeeId,(e, p) => new EmployeeProject{EmployeeName = e.Name,ProjectName = p.ProjectName}).ToList();
        }

        static List<Employee> GetUnassignedEmployees(List<Employee> employees, List<Project> projects)
        {
            var assignedEmployeeIds = projects.Select(p => p.EmployeeId).ToHashSet();
            return employees.Where(e => !assignedEmployeeIds.Contains(e.Id)).ToList();
        }

        static List<string> GetAllUniqueSkills(List<Employee> employees)
        {
            return employees.SelectMany(e => e.Skills).Distinct().ToList();
        }

        // 🔴 SECTION 4 – ADVANCED WORKFORCE ANALYTICS
        static List<DepartmentTopEmployees> GetTopEarnersByDepartment(List<Employee> employees)
        {
            return employees.GroupBy(e => e.Department)
                           .Select(g => new DepartmentTopEmployees 
                           { 
                               Department = g.Key,
                               TopEmployees = g.OrderByDescending(e => e.Salary).Take(3).ToList()
                           })
                           .ToList();
        }

        static List<Employee> RemoveDuplicateEmployees(List<Employee> employees)
        {
            return employees.GroupBy(e => e.Id).Select(g => g.First()).ToList();
        }

        static List<Employee> GetEmployeesByPage(List<Employee> employees, int pageNumber, int pageSize = 5)
        {
            return employees.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        // Test Runner
        static void TestAllSolutions(List<Employee> employees, List<Project> projects)
        {
            Console.WriteLine("=== 🟢 SECTION 1 – HR ANALYTICS ===");
            Console.WriteLine($"High earners (>60K): {GetHighEarningEmployees(employees).Count}");
            Console.WriteLine($"Employee names: {GetEmployeeNames(employees).Count}");
            Console.WriteLine($"Has HR employees: {HasHREmployees(employees)}\n");

            Console.WriteLine("=== 🟡 SECTION 2 – MANAGEMENT INSIGHTS ===");
            var deptCounts = GetDepartmentWiseCount(employees);
            Console.WriteLine("Department counts:");
            foreach (var dc in deptCounts)
                Console.WriteLine($"  {dc.Department}: {dc.Count}");
            
            var topEarner = GetHighestPaidEmployee(employees);
            Console.WriteLine($"Highest paid: {topEarner?.Name} (${topEarner?.Salary:N0})\n");

            Console.WriteLine("=== 🔵 SECTION 3 – PROJECT INTELLIGENCE ===");
            Console.WriteLine($"Employee-Project mappings: {GetEmployeeProjectMappings(employees, projects).Count}");
            Console.WriteLine($"Unassigned employees: {GetUnassignedEmployees(employees, projects).Count}");
            Console.WriteLine($"Unique skills: {GetAllUniqueSkills(employees).Count}\n");

            Console.WriteLine("=== 🔴 SECTION 4 – ADVANCED ANALYTICS ===");
            Console.WriteLine($"Top 3 earners per dept: {GetTopEarnersByDepartment(employees).Count} departments");
            Console.WriteLine($"Page 1 (first 5): {GetEmployeesByPage(employees, 0).Count} employees");
            Console.WriteLine($"Deduplicated: {RemoveDuplicateEmployees(employees).Count} employees (removed 1 duplicate)\n");
        }
    }
}
