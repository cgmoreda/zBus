using Microsoft.AspNetCore.Mvc;
using zBus.Data;

namespace zBus.Controllers
{
    public class DriverController : Controller
    {
        private readonly AppDbContext _context;
        public DriverController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Drivers.ToList();
            return View(data);
        }
    }
}
