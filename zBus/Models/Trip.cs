using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zBus.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }
        public double TripTime{ get; set;}
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public double TripPrice { get; set; }
        public List<bool> seatesStatus { get; set; }
        public int AvailableSeats { get; set; }
        [ForeignKey("Station")]
        public string DepartureCityID { get; set; }

        [ForeignKey("Station")]
         public string ArrivalCityID { get; set; }
        


    }
}
