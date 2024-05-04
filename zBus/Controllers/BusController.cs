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

       // عايزك تعمل الفانكشنز دي بس سيب details زي ما هي 
       // سيب الريتيرن زي ما هي رجعلي بس الداتا بعد كتخلص 
       // اعمل كده للباقي كله وسيب الفرونت 
        public IActionResult Details(int id)
        {
            var data = _context.Buses.ToList();
            return PartialView("_PartialviewBus",data);
        }
        // عايزك تعمل الفانكشنز دي بس سيب details زي ما هي 
        // سيب الريتيرن زي ما هي رجعلي بس الداتا بعد كتخلص 
        // اعمل كده للباقي كله وسيب الفرونت 
        public IActionResult Add()
        {
            var data = _context.Buses.ToList();

            return PartialView("_PartialviewBus", data);
        }

        public IActionResult Delete(int id)
        {
            var data = _context.Buses.FirstOrDefault(b=>b.BusId==id);
            
          
            return PartialView("_PartialviewBus", data);
        }


        public IActionResult Update(int id)
        {
            var data = _context.Buses.ToList();
            return PartialView("_PartialviewBus", data);
        }


        
    }
}
