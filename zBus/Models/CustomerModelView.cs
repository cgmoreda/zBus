namespace zBus.Models
{
    public class CustomerModelView
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public User User { get; set; }
        public IEnumerable<Trip> Trips { get; set; }
    }
}
