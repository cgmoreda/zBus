using System.ComponentModel.DataAnnotations.Schema;

namespace zBus.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string zib { get; set; }
        public double Price { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("User")]
        public int Userid { get; set; } 
        public User User { get; set; } 
        public ICollection<OrderItem> Items { get; set; }
    }
}
