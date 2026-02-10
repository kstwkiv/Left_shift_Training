using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Assignment_BoilerPlateCode.Models
{
    // =====================================================
    // DOMAIN MODELS
    // =====================================================
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public List<string> Skills { get; set; }
    }
}
