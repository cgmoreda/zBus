using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zBus.Models
{
    public class Bus
    {
        [Key]
        public int BusId { get; set; }

        [Required(ErrorMessage = "Photo is Required") ,Display(Name = "Photo Of Bus")]
        public string BusPicture { get; set; }
 

        [Required(ErrorMessage = "Bus Model is Required"), Display(Name = "Model Bus")]
        public string BusModel { get; set; }
        [Required(ErrorMessage = "Number Of Seats is Required"), Display(Name = "Number of Seats")]
        public int NumberOfSeats { get; set; }
        [Required(ErrorMessage = "AirConditioning Status is Required"),Display(Name = "Air Conditioning")]
        public bool? AirConditioningAvailable { get; set; }
        [Required(ErrorMessage = "Wifi Status is Required"),Display(Name = "Wifi Available ")]
        public bool? WifiAvailable { get; set; }
        [Required(ErrorMessage = "Restroom Status is Required")]
        public bool? RestroomAvailable { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        // Navigation property for the associated driver
        public virtual Driver? Driver { get; set; }
    }
}
