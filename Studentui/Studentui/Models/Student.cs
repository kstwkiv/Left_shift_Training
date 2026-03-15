using System.ComponentModel.DataAnnotations;

namespace Studentui.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Enter a valid phone number.")]
        [StringLength(20)]
        public string? Phone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Course is required.")]
        [StringLength(100)]
        public string Course { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Enrolment Date")]
        public DateTime EnrollDate { get; set; } = DateTime.Today;

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        // Computed helper
        public string FullName => $"{FirstName} {LastName}";
    }
}
