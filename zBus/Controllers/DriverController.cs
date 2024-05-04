using Microsoft.AspNetCore.Mvc;
using zBus.Data;
using zBus.Data.Services;
using zBus.Models;

namespace zBus.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDriversService _service;
        public DriverController(IDriversService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAll();
            return View("_PartialviewDriver", data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePicture,FullName,Age,YearsOfExperience")]Driver driver)
        {
            if (ModelState.IsValid)
            {
                _service.Add(driver);
                return RedirectToAction("Index");
            }
            return View(driver);
        }
    }
}
