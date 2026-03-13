using LINQ_Assignment_BoilerPlateCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Assignment_BoilerPlateCode.DTOs
{
    public class DepartmentTopEmployees
    {
        public string Department { get; set; }
        public List<Employee> TopEmployees { get; set; }
    }
}
