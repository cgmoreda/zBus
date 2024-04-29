using Microsoft.AspNetCore.Mvc;
using zBus.Data;

namespace zBus.Controllers
{
    public class SeatController : Controller
    {
        private readonly AppDbContext _context;
        public SeatController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
