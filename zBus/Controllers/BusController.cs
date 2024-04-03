using Microsoft.AspNetCore.Mvc;
using zBus.Data;

namespace zBus.Controllers
{
    public class BusController : Controller
    {
        private readonly AppDbContext _context;
        public BusController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var data = _context.Buses.ToList();
            ViewData["Drivers"] = _context.Drivers.ToList();
            return View(data);
        }
    }
}
