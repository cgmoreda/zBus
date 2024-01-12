using System.ComponentModel.DataAnnotations;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;

namespace zBus.Models
{
    public class Station
    {
        [Key]
        public int StationId { get; set; }
        string StationCity { get; set; }
        string StationAddress{ get; set; }
        string StationName { get; set; }
        string ContactNumber { get; set; }
    }
}
