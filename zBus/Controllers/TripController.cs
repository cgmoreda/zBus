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
using System.Data.OracleClient;
using Microsoft.AspNetCore.Authentication;
using zBus.Migrations;
using NuGet.Protocol;

namespace zBus.Controllers
{
    public class TripController : Controller
    {
        // private readonly ILogger<TripController> _logger;
        private readonly ITripService _TripService;
        private readonly IBusService _busService;
        private readonly IStationService _stationService;
        private readonly ISeatsService _seatService;
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly IUserService _userService;

        public TripController(ITripService tripService, IBusService busService, IStationService stationService,
            ISeatsService seatService, IOrderService orderService, IOrderItemService OrderItemService, IUserService userService)
        {
            //_logger = logger;
            _TripService = tripService;
            _busService = busService;
            _stationService = stationService;
            _seatService = seatService;
            _orderService = orderService;
            _orderItemService = OrderItemService;
            _userService = userService;
            _userService = userService;
        }

        [HttpGet]
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [ServiceFilter(typeof(RoleAuthorizationFilter))]
        public async Task<IActionResult> Index()
        {
            var trips = await _TripService.GetAll();
            return View("_PartialviewTrip", trips);
        }
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [ServiceFilter(typeof(RoleAuthorizationFilter))]
        public async Task<IActionResult> AddTrip()
        {
            var selected = await _stationService.GetAll();
            var Stations = selected.Select(i => new { i.StationId, i.StationName, i.StationCity }).ToList();
            var selsectedbus = await _busService.GetAll();
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
   
                };
                seats.Add(newSeat);
                _seatService.Add(newSeat);
            }
      
            ModelState["Seats"]!.ValidationState = ModelValidationState.Valid;
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
            trip = Modlestate(trip);
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
        public async Task<IActionResult> Update(int id)
        {
            var selected = await _stationService.GetAll();
            var Stations = selected.Select(i => new { i.StationId, i.StationName, i.StationCity }).ToList();
            var selsectedbus = await _busService.GetAll();
            TempData["Stations"] = JsonConvert.SerializeObject(Stations);
            TempData["Bus"] = JsonConvert.SerializeObject(selsectedbus);
            var trip = _TripService.GetById(id);
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
            return RedirectToAction("Admin", "User", new { id = 4 });
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
        //[ServiceFilter(typeof(LoginAuthorizationFilter))]
        public async Task<IActionResult> Book()
        {
            var tripsresuslt = await _TripService.GetAll();
            if (!TempData.ContainsKey("Stations"))
            {
                var station = await _stationService.GetAll();
                TempData["Stations"] = JsonConvert.SerializeObject(station);
                TempData.Keep("Stations");
            }
            if (tripsresuslt.Count() == 0)
            {
                return View("Empty");
            }
            else
            { return View(tripsresuslt); };
        }

        [HttpPost]
        public async Task<IActionResult> Searchtrip(int Departure, int Arrival, DateTime departuredate)
        {
            var tripsresuslt = await _TripService.Search(Departure, Arrival, departuredate);

            if (tripsresuslt.Count() == 0)
            {
                return View("Empty");
            }
            else
            {
                return View("Searshresultfind", tripsresuslt);
            }

        }


        public IActionResult Checkout(int id)
        {
            var tripresult = _TripService.GetById(id);
            return View("Checkout", tripresult);
        }
        public IActionResult Checkout_pay(List<int> selectedSeats, int tripid)
        {
            if (selectedSeats.Count() == 0)
            {return Json(new { staus = false });}
            TempData["selectedseats"]= JsonConvert.SerializeObject(selectedSeats); ;
            TempData["trip"] = tripid;
            TempData.Keep("selectedseats");
            TempData.Keep("trip");
            return Json(new { staus = true });
        }
        public IActionResult payment()
        {
            return View();
        }
        public async Task<IActionResult> payment_check(string name, string address, string city, string zip)
        {
                List<int> seats = JsonConvert.DeserializeObject<List<int>>(TempData["selectedseats"].ToString());
                int tripId = int.Parse(TempData["trip"]?.ToString() ?? "0");
                var userEmail = HttpContext.Session.GetString("UserEmail");
                var trip = _TripService.GetById(tripId);
                int quantity = seats.Count;
                double price = quantity * trip.TripPrice;
                 var orderItems =  new OrderItem { 
                  quantity = quantity,
                     TripId = tripId,
                       };
                var user = _userService.GetById(userEmail!);
                var order = new Order
                {
                    Price = price,
                    DateTime = DateTime.Now,
                    Userid = user.User_Id,
                    name = name,
                    address = address,
                    city = city,
                    zib = zip
                };
                  _seatService.Update(tripId, seats);
                  var createdOrder = _orderService.CreateOrder(order);
                  orderItems.OrderId = createdOrder.Id;
                  _orderItemService.CreateOrderItem(orderItems);
                  return Json(new { status = true, message = "Order placed successfully." });
        }
    }


   
}
