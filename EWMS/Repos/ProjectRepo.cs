using LINQ_Assignment_BoilerPlateCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Assignment_BoilerPlateCode.Repos
{
   public class ProjectRepo
    {
        public static List<Project> SeedProjects()
        {
            return new List<Project>()
            {
            new Project { ProjectId = 101, EmployeeId = 1, ProjectName = "Payroll Automation" },
            new Project { ProjectId = 102, EmployeeId = 4, ProjectName = "Customer Portal" },
            new Project { ProjectId = 103, EmployeeId = 5, ProjectName = "Cloud Migration" },
            new Project { ProjectId = 104, EmployeeId = 1, ProjectName = "Reporting Dashboard" }
             };
        }
    }
}
