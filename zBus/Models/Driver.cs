using System.ComponentModel.DataAnnotations;

namespace zBus.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        public string ProfilePicture { get; set; }
        public string FullName { get; set; }
        public string Contact { get; set; }
        public int Age { get; set; }
        public int YearsOfExperince { get; set; }
        
    }
}
