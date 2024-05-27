using System.ComponentModel.DataAnnotations;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace zBus.Models
{
    public class Station
    {
        public int StationId { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City name cannot exceed 50 characters")]
   
        public string StationCity { get; set; }

        [Required(ErrorMessage = "Address is required")]
   
        public string StationAddress { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Station name cannot exceed 100 characters")]
    
        public string StationName { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "Invalid contact number")]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }

        // Add photo property
       // [DataType(DataType.ImageUrl)]
        [DisplayName("Photo")]
        [Required(ErrorMessage = "Photo is required")]
        public string PhotoUrl { get; set; }

        [NotMapped]
        public ICollection<Trip>? ArrivalTrips { get; set; }
        [NotMapped]
        public ICollection<Trip>? DepartureTrips { get; set; }
    }
}
