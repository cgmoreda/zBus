using Microsoft.AspNetCore.Mvc;

using zBus.Data;
using zBus.Models;
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
            return PartialView("_PartialviewStation", data);
        }

       
        public IActionResult AddStation()
        {
            return View(new Station());
        }

        public IActionResult Valid_Add(Station station)
        {

            if(ModelState.IsValid)
            {
                _context.Stations.Add(station);
                return RedirectToAction("Index");

            }
            else
            {
                return View("Add", station);
            }


        }


    }
}
