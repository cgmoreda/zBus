using Microsoft.AspNetCore.Mvc;

using zBus.Data;
namespace zBus.Controllers
{
    public class StationController :Controller
    { 
        private readonly AppDbContext _context; 
        public StationController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Stations.ToList();
            return View();
        }
    }
}
