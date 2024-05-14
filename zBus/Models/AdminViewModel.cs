namespace zBus.Models
{
    public class AdminViewModel
    {
        public int Id { get; set; }
        public IEnumerable<Bus> Buses { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Station> Stations { get; set; }
        public IEnumerable<TripDetails> Trips { get; set; }
    }
}
