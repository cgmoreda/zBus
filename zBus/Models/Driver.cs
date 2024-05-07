using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace zBus.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePicturePath { get; set; }
        
        // Change type to string
        [Required(ErrorMessage = "Required Feild")]
        [MinLength(3, ErrorMessage = "At least 3 charachters")]
        [MaxLength(20, ErrorMessage = "maximam 20 charachters")]
        [Display(Name = "First Name")]
        public string Fisrt_name { get; set; }



        [Required(ErrorMessage = "Required Feild")]
        [MinLength(3, ErrorMessage = "At least 3 charachters")]
        [MaxLength(20, ErrorMessage = "maximam 20 charachters")]
        [Display(Name = "Last Name")]
        public string Last_name { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "Invalid contact number")]
        [DisplayName("Contact Number")]
        [Required(ErrorMessage = "Required Feild")]
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
