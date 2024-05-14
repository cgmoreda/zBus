using System.Security.Policy;

namespace zBus.Models
{
    
    public class TripDetails
    {
    
        public int Id { get; set; }
        public IEnumerable<Seat> Seats { get; set; }
        public int AvailableSeatsCount { get; set; }
        public string arrivalstation { get; set; }
        public int Allseatscount { get; set; }
        public string depturestation { get; set; }

        public Trip trip {  get; set; } 
    }
}
