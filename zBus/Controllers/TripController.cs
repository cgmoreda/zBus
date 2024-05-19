using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using zBus.Data;
using zBus.Data.Enums;
using zBus.Data.Services;
using zBus.GLobal;
using zBus.Models;
using Microsoft.AspNetCore.Authorization;
using zBus.Filters;

namespace zBus.Controllers
{
    public class TripController : Controller
    {
       // private readonly ILogger<TripController> _logger;
        private readonly ITripService _TripService;
        private readonly IBusService _busService;
        private readonly IStationService _stationService;
        private readonly ISeatsService _seatService;

        public TripController(ITripService tripService, IBusService busService, IStationService stationService, ISeatsService seatService)
        {
            //_logger = logger;
            _TripService = tripService;
            _busService = busService;
            _stationService = stationService;
            _seatService = seatService;
        }

        [HttpGet]
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [ServiceFilter(typeof(RoleAuthorizationFilter))]
        public async Task<IActionResult> Index()
        {
            var trips = await _TripService.GetAll();
            List<TripDetails> ViewModel = new List<TripDetails>();
            foreach (var trip in trips)
            {
                var TripDetails = new TripDetails();
                TripDetails.Seats = _seatService.GetById(trip.TripId);
                TripDetails.AvailableSeatsCount = TripDetails.Seats.Count(s => s.Status == SeatStatus.Available);
                TripDetails.arrivalstation = _stationService.GetById(trip.ArrivalStationID).StationName;
                TripDetails.depturestation = _stationService.GetById(trip.DepartureStationID).StationName;
                TripDetails.trip = trip;
                TripDetails.Id = trip.TripId;
                TripDetails.Allseatscount = _busService.GetById(trip.BusId).NumberOfSeats;
                ViewModel.Add(TripDetails);

            }
            //return View(ViewModel);
            return View("_PartialviewTrip", ViewModel);
        }
       
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [ServiceFilter(typeof(RoleAuthorizationFilter))]
        public async Task<IActionResult> AddTrip()
        {
            var selected = await _stationService.GetAll();
            var Stations = selected.Select(i => new { i.StationId, i.StationName, i.StationCity }).ToList();
            var selsectedbus= await _busService.GetAll();
            TempData["Stations"] = JsonConvert.SerializeObject(Stations);
            TempData["Bus"] = JsonConvert.SerializeObject(selsectedbus);
            return View(new Trip());
        }

        public Trip Modlestate(Trip trip)
        {   
          
            int numberOfSeats = _busService.GetById(trip.BusId).NumberOfSeats;
            var seats = new List<Seat>();
            for (int i = 1; i <= numberOfSeats; i++)
            {
                var newSeat = new Seat
                {
                    Status = SeatStatus.Available,
                    TripId = trip.TripId,
                    Trip=trip,
                   
                };
                seats.Add(newSeat);
                _seatService.Add(newSeat);
            }


           
            trip.Users=(new List<User>());
            trip.ArrivalStation = _stationService.GetById(trip.ArrivalStationID);
            trip.DepartureStation = _stationService.GetById(trip.DepartureStationID);
            trip.DepartureStation= _stationService.GetById(trip.DepartureStationID);
            trip.Bus = _busService.GetById(trip.BusId);
           // ModelState["Seats"]!.ValidationState = ModelValidationState.Valid;
            ModelState["Users"]!.ValidationState = ModelValidationState.Valid;
            ModelState["ArrivalStation"]!.ValidationState = ModelValidationState.Valid;
            ModelState["DepartureStation"]!.ValidationState = ModelValidationState.Valid;
            ModelState["Bus"]!.ValidationState = ModelValidationState.Valid;
            return trip;
        }
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [ServiceFilter(typeof(RoleAuthorizationFilter))]
        public IActionResult Valid_Add(Trip trip)
        {
            trip=Modlestate(trip);
            if (ModelState.IsValid)
            {
                _TripService.Add(trip);

                return RedirectToAction("Admin", "User", new { id = 4 });
            }
            return RedirectToAction("AddTrip", trip);
        }

        [HttpGet]
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [ServiceFilter(typeof(RoleAuthorizationFilter))]
        public async Task< IActionResult> Update(int id)
        {
            var selected = await _stationService.GetAll();
            var Stations = selected.Select(i => new { i.StationId, i.StationName, i.StationCity }).ToList();
            var selsectedbus = await _busService.GetAll();
            TempData["Stations"] = JsonConvert.SerializeObject(Stations);
            TempData["Bus"] = JsonConvert.SerializeObject(selsectedbus);
            var trip=_TripService.GetById(id);
            return View(trip);
        }
        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [ServiceFilter(typeof(RoleAuthorizationFilter))]
        public IActionResult Valid_Update(Trip trip)
        {
            Modlestate(trip);
            if (ModelState.IsValid)
            {
                _TripService.Add(trip);
                return RedirectToAction("Admin", "User", new { id = 4 });
            }
            return RedirectToAction("AddTrip", trip);
        }

        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [ServiceFilter(typeof(RoleAuthorizationFilter))]
        public IActionResult Delete(int id)
        {
            _TripService.Delete(id);
            return RedirectToAction("Admin", "User", new {id=4});
        }


		public IActionResult Check_log()
		{
			if (GlobalVariables.Login_Status)
			{
                return RedirectToAction("Book");
			}
			else
			{
				return Json(new { loggedIn = false });
			}
		}
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [HttpGet]
        public async Task <IActionResult> Book()
		{
            var tripsresuslt = await _TripService.GetAll();
            if(!TempData.ContainsKey("Stations"))
            {
                var station = await _stationService.GetAll();
                TempData["Stations"]= JsonConvert.SerializeObject(station);
                TempData.Keep("Stations");
            }

            if (tripsresuslt.Count() == 0)
            {
                return View("Empty");
            }
            else
            {
                List<TripDetails> ViewModel = new List<TripDetails>();
                foreach (var trip in tripsresuslt)
                {
                    var TripDetails = new TripDetails();
                    TripDetails.Seats = _seatService.GetById(trip.TripId);
                    TripDetails.AvailableSeatsCount = TripDetails.Seats.Count(s => s.Status == SeatStatus.Available);
                    TripDetails.arrivalstation = _stationService.GetById(trip.ArrivalStationID).StationName;
                    TripDetails.depturestation = _stationService.GetById(trip.DepartureStationID).StationName;
                    TripDetails.trip = trip;
                    TripDetails.Id = trip.TripId;
                    TripDetails.Allseatscount = _busService.GetById(trip.BusId).NumberOfSeats;
                    ViewModel.Add(TripDetails);

                }
                return View(ViewModel);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Searchtrip(int Departure, int Arrival, DateTime departuredate)
        { 
            var tripsresuslt = await _TripService.Search(Departure, Arrival, departuredate);

            if(tripsresuslt.Count()==0)
            {
                return View("Empty");
            }
            else
            {
                List<TripDetails> ViewModel = new List<TripDetails>();
                foreach (var trip in tripsresuslt)
                {
                    var TripDetails = new TripDetails();
                    TripDetails.Seats = _seatService.GetById(trip.TripId);
                    TripDetails.AvailableSeatsCount= TripDetails.Seats.Count(s=>s.Status==SeatStatus.Available);
                    TripDetails.arrivalstation=_stationService.GetById(trip.ArrivalStationID).StationName;
                    TripDetails.depturestation = _stationService.GetById(trip.DepartureStationID).StationName;
                    TripDetails.trip = trip;
                    TripDetails.Id = trip.TripId;
                    TripDetails.Allseatscount = _busService.GetById(trip.BusId).NumberOfSeats;
                    ViewModel.Add(TripDetails);

                }

                return View("Searshresultfind", ViewModel); 
            
            }
           
        }


        public IActionResult Checkout(TripDetails trip)
        {
            trip.Seats = _seatService.GetById(trip.Id);
            ViewBag.price = _TripService.GetById(trip.Id).TripPrice;

            return View("Checkout", trip);
        }

        public IActionResult Checkout_pay(List<int> selectedSeats, int trip)
        {
            if(selectedSeats.Count()==0)
            {
                return Json(new { staus = false });
            }
            _seatService.Update(trip, selectedSeats);
            return Json(new { staus = true } );
        }
    }
}
