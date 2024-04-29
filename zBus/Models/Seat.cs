
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using zBus.Data.Enums;

public class Seat
{
    [Key]
    public int SeatId { get; set; }

    public SeatStatus Status { get; set; }
    
    // add id if occupied?

    [ForeignKey("Trip")]
    public int TripId { get; set; }

    public virtual Trip? Trip { get; set; }
}