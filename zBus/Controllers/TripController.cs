using Microsoft.AspNetCore.Mvc;
using zBus.Data;

namespace zBus.Controllers
{
    public class TripController : Controller
    {
        private readonly AppDbContext _context;
        public TripController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Trips.ToList();
            return View();
        }
    }
}
