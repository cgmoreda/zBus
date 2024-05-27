
//using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using zBus.Data;
using zBus.Data.Services;
using zBus.Filters;
using zBus.Models;

namespace zBus.Controllers
{
    [ServiceFilter(typeof(LoginAuthorizationFilter))]
    [ServiceFilter(typeof(RoleAuthorizationFilter))]
    public class BusController : Controller
    {
        private readonly IBusService _service;
        private readonly IDriversService _DriverService;
        public IWebHostEnvironment _webHostEnvironment;
        public BusController(IBusService service, IWebHostEnvironment webHostEnvironment, IDriversService DriverService)
        {
            _webHostEnvironment = webHostEnvironment;
            _DriverService = DriverService;
            _service = service;

        }


        public async Task<IActionResult> Details()
        {
            var data = await _service.GetAll();
            return PartialView("_PartialviewBus", data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var drivers = _DriverService.GetAll();
            if (!TempData.ContainsKey("Drivers"))
            {
                TempData["Drivers"] = JsonConvert.SerializeObject(drivers);
                TempData.Keep("Drivers");
            }
            return View(new Bus());
        }
        [HttpPost]
        public IActionResult Valid_Add(Bus Bus, IFormFile photo)
        {

            ModelState["Driver"]!.ValidationState = ModelValidationState.Valid;
            if (!TempData.ContainsKey("Drivers"))
            {
                var drivers = _DriverService.GetAll();
                TempData["Drivers"] = JsonConvert.SerializeObject(drivers);
                TempData.Keep("Drivers");
            }
            if (ModelState.IsValid)
            {
                Bus.Driver = _DriverService.GetById(Bus.DriverId);
                if (photo != null)
                {
                    if (photo.ContentType.StartsWith("image/"))
                    {
                        string fileName = Guid.NewGuid().ToString() + "-" + photo.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Bus");
                        string filePath = Path.Combine(serverFolder, fileName);
                        using (var filestream = new FileStream(filePath, FileMode.Create))
                        {
                            photo.CopyTo(filestream);
                        }
                        Bus.BusPicture = "/Bus/" + fileName;
                        _service.Add(Bus);
            
                        return RedirectToAction("Admin", "User", new { id = 1 });
                    }
                 
                        ModelState.AddModelError("BusPicture", "You must Upload a Photo");
                        return View("Add", Bus);
                    
                }
                ModelState.AddModelError("BusPicture", "You must Upload a Photo");
                return View("Add", Bus);

            }
            else
            {
                ModelState.AddModelError("BusPicture", "You must Upload a Photo");
                return View("Add", Bus);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool check = _service.Delete(id);
            if (check)
            {
                return RedirectToAction("Admin", "User", new { id = 1 });
            }
            {
                //var bus = _service.GetAll();
                return Json(new { loggedIn = false });
            }
        }


        public IActionResult Update(int id)
        {
            if (!TempData.ContainsKey("Drivers"))
            {
                var drivers = _DriverService.GetAll();
                TempData["Drivers"] = JsonConvert.SerializeObject(drivers);
                TempData.Keep("Drivers");
            }
            var bus = _service.GetById(id);
            return View(bus);
        }

        [HttpPost]
        public IActionResult Update_Add(Bus Bus, IFormFile photo, int id)
        {
            if (!TempData.ContainsKey("Drivers"))
            {
                var drivers = _DriverService.GetAll();
                TempData["Drivers"] = JsonConvert.SerializeObject(drivers);
                TempData.Keep("Drivers");
            }
            ModelState["Driver"]!.ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                Bus.Driver = _DriverService.GetById(Bus.DriverId);
                if (photo != null)
                {
                    if (photo.ContentType.StartsWith("image/"))
                    {
                        string fileName = Guid.NewGuid().ToString() + "-" + photo.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Bus");
                        string filePath = Path.Combine(serverFolder, fileName);
                        using (var filestream = new FileStream(filePath, FileMode.Create))
                        {
                            photo.CopyTo(filestream);
                        }
                        Bus.BusPicture = "/Bus/" + fileName;
                    }
                    else
                    {
                        ModelState.AddModelError("BusPicture", "You must Upload a Photo");
                        return View("Update", Bus);
                    }
                    _service.Update(id, Bus);
                   
                    return RedirectToAction("Admin", "User", new {id=1});
                }

                ModelState.AddModelError("BusPicture", "You must Upload a Photo");
                return View("Update", Bus);
            }
            else
            {
                ModelState.AddModelError("BusPicture", "You must Upload a Photo");
                return View("Update", Bus);
            }
        }
    }

}