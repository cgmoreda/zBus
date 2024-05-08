using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using zBus.Data;
using zBus.Data.Services;
using zBus.Models;
namespace zBus.Controllers
{
    public class StationController :Controller
    { 
        private readonly IStationService _stationService;
        public IWebHostEnvironment _webHostEnvironment;
        public StationController(IStationService stationService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _stationService = stationService;
        }
        public async Task<IActionResult>Index()
        {
            var data = await _stationService.GetAll();
        
            return PartialView("_PartialviewStation", data);
        }
       
        public IActionResult AddStation()
        {
            return View(new Station());
        }

        public IActionResult Delete(int id)
        {
            _stationService.Delete(id);
            return RedirectToAction("Admin", "User");
        }
       
        public IActionResult Valid_Add(Station station, IFormFile photo)
            {
            ModelState["PhotoUrl"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                if(photo != null)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + photo.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Station");
                    string filePath = Path.Combine(serverFolder, fileName);
                    using(var filestream= new FileStream(filePath,FileMode.Create))
                    {
                        photo.CopyTo(filestream);
                    }
                    station.PhotoUrl = "/Station/"+fileName;

                }
                _stationService.Add(station);
                return  RedirectToAction("Admin", "User");
                // return PartialView("_PartialviewStation", station);
            }
            else
            {
                return View("AddStation", station);
            }
        }
       
        public IActionResult Update( int id)
        {
           var station= _stationService.GetById(id);
            return View(station);
        }
       
        public IActionResult Update_Valid(Station station, IFormFile photo, int id)
        {
            ModelState["PhotoUrl"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {

                if (photo != null)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + photo.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Station");
                    string filePath = Path.Combine(serverFolder, fileName);
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(filestream);
                    }
                    station.PhotoUrl = "/Station/" + fileName;

                }
               
                _stationService.Update(id, station);
                return RedirectToAction("Admin", "User");
                // return PartialView("_PartialviewStation", station);

            }
            else
            {
                return View("Update", station);
            }


        }


    }
}
