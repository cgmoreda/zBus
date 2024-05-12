using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using zBus.Models;
using zBus.Data.Enums;

public class Trip
{
    [Key]
    public int TripId { get; set; }

    [Required(ErrorMessage = "Departure time is required.")]
    public DateTime DepartureTime { get; set; }

    [Required(ErrorMessage = "Arrival time is required.")]
    public DateTime ArrivalTime { get; set; }

    [Required(ErrorMessage = "Trip price is required.")]
    public double TripPrice { get; set; }

    [ForeignKey("DepartureStation")]
    [Required(ErrorMessage = "Departure Station is required.")]
    public int DepartureStationID { get; set; }

    [ForeignKey("ArrivalStation")]
    [Required(ErrorMessage = "Arrival Station is required.")]
    public int ArrivalStationID { get; set; }

    [ForeignKey("Bus")]
    [Required(ErrorMessage = "Bus Trip is required.")]
    public int BusId { get; set; }

    // Navigation properties
    [Display(Name = "Departure Station")]
    public virtual Station DepartureStation { get; set; }

    public virtual Station ArrivalStation { get; set; }

    public virtual Bus Bus { get; set; }

   // public virtual ICollection<Seat> Seats { get; set; }

    public virtual ICollection<User> Users { get; set; }
}
