using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using zBus.Models;
using zBus.Data.Enums;
public class Trip
{
    [Key]
    public int TripId { get; set; }
    public double TripTime { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public double TripPrice { get; set; }

    [ForeignKey("DepartureStation")]
    public int DepartureCityID { get; set; }

    [ForeignKey("ArrivalStation")]
    public int ArrivalCityID { get; set; }
    
    [ForeignKey("Bus")]
    public int BusId{ get; set; }

    // Navigation properties
    [Display(Name = "Departure Station")]
    public virtual Station DepartureStation { get; set; }

    public virtual Station ArrivalStation { get; set; }

    public virtual Bus Bus{ get; set; }
    public virtual ICollection<Seat> Seats { get; set; }

    public virtual ICollection<User> Users { get; set; }
}
