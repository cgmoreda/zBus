using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using zBus.Data;
using zBus.Data.Services;
using zBus.Models;

namespace zBus.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDriversService _service;
        public IWebHostEnvironment _webHostEnvironment;
        public DriverController(IDriversService service, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _service = service;
          
        }
        public IActionResult Index()
        {
    
            var data =  _service.GetAll();
            return PartialView("_PartialviewDriver", data);
        }
       
       
        public IActionResult AddDriver()
        {
            return View(new Driver());
        }

        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Admin", "User");
        }
      
        public IActionResult Valid_Add(Driver Driver, IFormFile photo)
        {
            ModelState["ProfilePicturePath"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + photo.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Driver");
                    string filePath = Path.Combine(serverFolder, fileName);
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(filestream);
                    }
                    Driver.ProfilePicturePath = "/Driver/" + fileName;

                }
                _service.Add(Driver);
                return RedirectToAction("Admin", "User");
                // return PartialView("_PartialviewStation", station);
            }
            else
            {
                return View("AddDriver", Driver);
            }
        }
        
        public IActionResult Update(int id)
        {
            var Driver = _service.GetById(id);
            return View(Driver);
        }
       
        public IActionResult Update_Valid(Driver Driver, IFormFile photo, int id)
        {
            ModelState["ProfilePicturePath"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {

                if (photo != null)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + photo.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Driver");
                    string filePath = Path.Combine(serverFolder, fileName);
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(filestream);
                    }
                    Driver.ProfilePicturePath = "/Driver/" + fileName;

                }

                _service.Update(id, Driver);
                return RedirectToAction("Admin","User");
                // return PartialView("_PartialviewStation", station);

            }
            else
            {
                return View("Update", Driver);
            }


        }


    }
}
