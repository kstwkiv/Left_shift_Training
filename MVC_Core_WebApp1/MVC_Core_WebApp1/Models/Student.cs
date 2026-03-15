using MVC_Core_WebApp1.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_Core_WebApp1.Models
{
    public class Student
    {
        [Required(ErrorMessage = "roll no can't be left blank")]
        public int RollNo { get; set; }
        [Required(ErrorMessage = "name can't be left blank")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "name must be between 2 and 15 characters")]
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        [Range(18, 60, ErrorMessage = "age must be between 18 and 60")]
        public int Age { get; set; }
    }
}

