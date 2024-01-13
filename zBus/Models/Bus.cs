using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zBus.Models
{
    public class Bus
    {
        [Key]
        public int BusId { get; set; }

        public string BusPicture { get; set; }
        public string BusModel { get; set; }
        public int NumberOfSeats { get; set; }
        public bool AirConditioningAvailable { get; set; }
        public bool WifiAvailable { get; set; }
        public bool RestroomAvailable { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        // Navigation property for the associated driver
        public virtual Driver Driver { get; set; }
    }
}
