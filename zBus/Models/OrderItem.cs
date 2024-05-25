using System.ComponentModel.DataAnnotations.Schema;

namespace zBus.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int quantity { get; set; }
        [ForeignKey("trip")]
        public int TripId { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Trip Trip { get; set; } 
        
    }
}
