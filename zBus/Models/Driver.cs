using System.ComponentModel.DataAnnotations;

namespace zBus.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePicturePath { get; set; } // Change type to string

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name Must be between 3 and 50 chars")]
        public string FullName { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Contact Number is required")]
        public string Contact { get; set; }

        [Display(Name = "Age")]
        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        [Display(Name = "Years Of Experience")]
        [Range(1, 45, ErrorMessage = "Years of Experience must be between 1 and 50")]
        [Required(ErrorMessage = "Years of Experience is required")]
        public int YearsOfExperience { get; set; }
    }
}
