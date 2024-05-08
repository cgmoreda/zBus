using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using zBus.Data;
using zBus.Data.Enums;
using zBus.Data.Services;
using zBus.Models;

namespace zBus.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripService _TripService;
        private readonly IBusService _busService;
        private readonly IStationService _stationService;
     
        public TripController(ITripService TripService, IBusService busService, IStationService stationService)
        {
            _TripService = TripService;
            _busService = busService;
            _stationService = stationService;
        }
        public IActionResult Index()
        {
            
            
            return View("_PartialviewTrip");
        }

        public IActionResult Details()
        {
           
            return View();

        }

        public async Task<IActionResult> AddTrip()
        {
            var selected = await _stationService.GetAll();
            var Stations = selected.Select(i => new { i.StationId, i.StationName, i.StationCity }).ToList();
            var selsectedbus= await _busService.GetAll();
            TempData["Stations"] = JsonConvert.SerializeObject(Stations);
            TempData["Bus"] = JsonConvert.SerializeObject(selsectedbus);
            return View(new Trip());
        }

        public void Modlestate(Trip trip)
        { 
            int NOS = _busService.GetById(trip.BusId).NumberOfSeats;
            var seats = new List<Seat>();
            for (int i = 1; i <= NOS; i++)
            {
                seats.Add(new Seat
                {
                    SeatId = i,
                    Status = SeatStatus.Available,
                });
            }
            trip.Seats = seats;
            trip.Users = new List<User>();
            trip.ArrivalStation = _stationService.GetById(trip.ArrivalStationID);
            trip.DepartureStation = _stationService.GetById(trip.DepartureStationID);
            trip.Bus = _busService.GetById(trip.BusId);
            ModelState["Seats"]!.ValidationState = ModelValidationState.Valid;
            ModelState["Users"]!.ValidationState = ModelValidationState.Valid;
            ModelState["ArrivalStation"]!.ValidationState = ModelValidationState.Valid;
            ModelState["DepartureStation"]!.ValidationState = ModelValidationState.Valid;
            ModelState["Bus"]!.ValidationState = ModelValidationState.Valid;
        }
        public IActionResult Valid_Add(Trip trip)
        {
            Modlestate(trip);
            if (ModelState.IsValid)
            {
                _TripService.Add(trip);
                return RedirectToAction("Admin", "User");
            }
            return RedirectToAction("AddTrip", trip);
        }


    }
}
