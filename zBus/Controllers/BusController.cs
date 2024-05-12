
//using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using zBus.Data;
using zBus.Data.Services;
using zBus.Models;

namespace zBus.Controllers
{
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

        public IActionResult Add()
        { 
            var drivers = _DriverService.GetAll();
            TempData["Drivers"] = JsonConvert.SerializeObject(drivers);
            TempData.Keep("Drivers");
           ViewBag.photo=string.Empty;
            return View(new Bus());
        }
        public IActionResult Valid_Add(Bus Bus, IFormFile photo)
        {
            ModelState["BusPicture"].ValidationState = ModelValidationState.Valid;
            ModelState["Driver"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    if(photo.ContentType.StartsWith("image/"))
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
                    else { 
                       
                        ViewBag.photo= $"You Uploaded {photo.ContentType.Split('/').Last()} Try (.jpg - .png - .jpeg )";
                        return View("Add", Bus);
                    }
                }
                _service.Add(Bus);
                return RedirectToAction("Admin", "User");
                // return PartialView("_PartialviewStation", station);
            }
            else
            {
                ViewBag.photo = $"You Must Upload Photo Try";
                return View("Add", Bus);
            }
        }


        public IActionResult Delete(int id)
        {
            bool check = _service.Delete(id);
            if (check)
            {
                return Json(new { loggedIn = true });
            }
            {
                //var bus = _service.GetAll();
                return Json(new { loggedIn = false });
            }  
        }


        public IActionResult Update(int id)
        {
            var drivers = _DriverService.GetAll();
            TempData["Drivers"] = JsonConvert.SerializeObject(drivers);
            TempData.Keep("Drivers");
            var bus= _service.GetById(id);
            ViewBag.photo = string.Empty;
            return View(bus);
        }


        public IActionResult Update_Add(Bus Bus, IFormFile photo,int id)
        {
            ModelState["BusPicture"].ValidationState = ModelValidationState.Valid;
            ModelState["Driver"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
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

                        ViewBag.photo = $"You Uploaded {photo.ContentType.Split('/').Last()} Try (.jpg - .png - .jpeg )";
                        return View("Add", Bus);
                    }
                }
                _service.Update(id,Bus);
                return RedirectToAction("Admin", "User");
                // return PartialView("_PartialviewStation", station);
            }
            else
            {
                return View("Add", Bus);
            }
        }

       



    }
}
