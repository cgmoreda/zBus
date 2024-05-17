
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using zBus.Data.Enums;

public class Seat
{
    [Key]
    public int SeatId { get; set; }

    public SeatStatus Status { get; set; }

    public int TripId { get; set; }

    // Navigation property
    [ForeignKey("TripId")]
    public virtual Trip Trip { get; set; }
}