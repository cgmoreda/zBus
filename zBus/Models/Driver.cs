using System.ComponentModel.DataAnnotations;

namespace zBus.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Contact Number")]
        public string Contact { get; set; }
       
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Years Of Experince")]
        public int YearsOfExperince { get; set; }
        
    }
}
