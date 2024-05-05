using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zBus.Models
{
    public class Bus
    {
        [Key]
        public int BusId { get; set; }

        [Required, Display(Name = "Photo Of Bus")]
        public string BusPicture { get; set; }

        [Required, Display(Name = "Model OF Bus")]
        public string BusModel { get; set; }
        [Required, Display(Name = "Number Of Seats")]
        public int NumberOfSeats { get; set; }
        [Display(Name = "Air Conditioning")]
        public bool? AirConditioningAvailable { get; set; }
        [Display(Name = "Wifi Available ")]
        public bool? WifiAvailable { get; set; }
        public bool? RestroomAvailable { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        // Navigation property for the associated driver
        public virtual Driver Driver { get; set; }
    }
}
