using System.ComponentModel.DataAnnotations;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;

namespace zBus.Models
{
    public class Station
    {
        [Key]
        public int StationId { get; set; }
        public string StationCity { get; set; }
        public string StationAddress { get; set; }
        public string StationName { get; set; }
        public string ContactNumber { get; set; }
    }
}
