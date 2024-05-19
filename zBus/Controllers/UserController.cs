using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Newtonsoft.Json;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using zBus.Data.Enums;
using zBus.Data.Services;
using zBus.Filters;
using zBus.GLobal;
using zBus.Models;
using static System.Collections.Specialized.BitVector32;

namespace zBus.Controllers
{
 
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        public IWebHostEnvironment _webHostEnvironment;
        private readonly ITripService _TripService;
        private readonly IBusService _busService;
        private readonly IStationService _stationService;
        private readonly IDriversService _driversService;
        private readonly ISeatsService _seatsService;
        
        public UserController(IUserService userService, IWebHostEnvironment webHostEnvironment, ITripService TripService
            , IBusService busService, IStationService stationService, IDriversService driversService, ISeatsService seatsService)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            _TripService = TripService;
            _busService = busService;
            _stationService = stationService;
            _driversService = driversService;
            _seatsService = seatsService;
           
           
        }

        public IActionResult Login_Page()
        {

            return View("Login_Page");
        }

        public IActionResult Register()
        {

            return View(new User());

        }
        public IActionResult Register_Save(User user, IFormFile photo)
        {
            if (!_userService.Exist(user.Email))
            {
                if (ModelState.IsValid)
                {
                    if (photo != null)
                    {

                        if (photo.ContentType.StartsWith("image/"))
                        {
                            string fileName = Guid.NewGuid().ToString() + "-" + photo.FileName;
                            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, "User");
                            string filePath = Path.Combine(serverFolder, fileName);
                            using (var filestream = new FileStream(filePath, FileMode.Create))
                            {
                                photo.CopyTo(filestream);
                            }
                            user.PhotoPhath = "/User/" + fileName;
                            _userService.Add(user);
                            EmailService _emailService = new EmailService();
                             _emailService.SendRegistrationEmailAsync(user.Email, user.Fisrt_name);
                            return View("Login_Page");
                        }
                    }
                    ModelState.AddModelError("PhotoPhath", "Add Vaild Photo");
                    return View("Register", user);
                }

                return View("Register", user);
            }
            else
            {

                ModelState.AddModelError("email", "Email Already Exists Try Another one ");
                return View("Register",user);
            }
           
        }

            
        public IActionResult Login_Valid(string email, string Password)
        {
            
            if (ModelState.IsValid)
            {
                if (_userService.Exist(email))
                {
                    var user = _userService.GetById(email);
                    if (user.Password == Password)
                    {
                        GlobalVariables.Login_Status = true;
                        GlobalVariables.User = user.Email;
                        // get user role
                        if(user.Admin==true)
                        { HttpContext.Session.SetString("UserRole", "Admin"); }
                        else
                        {
                            HttpContext.Session.SetString("UserRole", "Customer");
                        }
                        
                        HttpContext.Session.SetString("UserEmail", user.Email);
                        return RedirectToAction("Index", "Home");
                    }

                    {
                        ModelState.AddModelError("password", "Password is not Correct");
                        return View("Login_Page");
                    }
                }
                else
                {
                    ModelState.AddModelError("email", "Email does not Exist ");
                    return View("Login_Page");
                }
            }

            return View("Login_Page");

        }

        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserRole");
            HttpContext.Session.Remove("UserEmail");
            GlobalVariables.Login_Status = false;
            GlobalVariables.User = String.Empty;
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Account()
        {

            var user = _userService.GetById(GlobalVariables.User!);
            return View(user);
        }


        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        public IActionResult Delete()
        {
            _userService.Delete(GlobalVariables.User!);
            return RedirectToAction("Index", "Home");
        }
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [HttpGet]
        public IActionResult Update_Data()
        {
            var UserEmail = HttpContext.Session.GetString("UserEmail");
            var user = _userService.GetById(UserEmail);
            return PartialView("Update_Data", user);
        }
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        public IActionResult Update_Pass()
        {
            var UserEmail = HttpContext.Session.GetString("UserEmail");

            var user = _userService.GetById(UserEmail);
            return PartialView("Update_Pass", user);
        }

        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        public IActionResult UpdatePass(string Password)
        {
            try
            {
                var UserEmail = HttpContext.Session.GetString("UserEmail");
                _userService.Update_Pass(Password!, UserEmail);
            }
            catch { }

            return RedirectToAction("Admin", 0);
           
        }




        public void SendAuthentication()
        {
            var UserEmail = HttpContext.Session.GetString("UserEmail");
            string username = _userService.GetById(UserEmail).Fisrt_name;
            EmailService emailService = new EmailService();
            emailService.SendPasswordChangeEmailAsync(UserEmail, username);
        }
        public IActionResult AuthenticationCheck(string code)
        {
            if(code==GlobalVariables.Code)
            {
                return Json(new { flag = true });
            }
            return Json(new { flag = false });
        }
        [ServiceFilter(typeof(LoginAuthorizationFilter))]

        public IActionResult UpdateData(User user, IFormFile photo)
        {
            ModelState["Password"].ValidationState = ModelValidationState.Valid;
            if (ModelState["PhotoPhath"].ValidationState== ModelValidationState.Valid && ModelState["photo"].ValidationState== ModelValidationState.Invalid)
            {
                ModelState["photo"].ValidationState = ModelValidationState.Valid;
            }
            if (ModelState.IsValid)
                {

                if (photo != null)
                { 

                    if (photo.ContentType.StartsWith("image/"))
                    {
                        string fileName = Guid.NewGuid().ToString() + "-" + photo.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, "User");
                        string filePath = Path.Combine(serverFolder, fileName);
                        using (var filestream = new FileStream(filePath, FileMode.Create))
                        {
                            photo.CopyTo(filestream);
                        }
                        user.PhotoPhath = "/User/" + fileName;
                        _userService.Update(user.Email,user);
                        return RedirectToAction("Admin", 0);
                    } 
                    
                }
                else if (!String.IsNullOrEmpty(user.PhotoPhath))
                {
                    _userService.Update(user.Email, user);
                    return RedirectToAction("Admin",0);
                }
                ModelState.AddModelError("PhotoPhath", "Add Vaild Photo");
                return View("Update_Data", user);
            }
            return View("Update_Data", user);
        }


        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        public IActionResult Book()
        {

            return View();
        }
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [ServiceFilter(typeof(RoleAuthorizationFilter))]
        public async Task<IActionResult> Admin(int id = 0)
        {
            var UserEmail = HttpContext.Session.GetString("UserEmail");
            if (UserEmail == null)
            {

            }
            var buses = await _busService.GetAll();
            var drivers = await _driversService.GetAll();
            var stations = await _stationService.GetAll();
            var trips = await _TripService.GetAll();
            List<TripDetails> tripdetails = new List<TripDetails>();
            foreach (var trip in trips)
            {
                var TripDetails = new TripDetails();
                TripDetails.Seats = _seatsService.GetById(trip.TripId);
                TripDetails.AvailableSeatsCount = TripDetails.Seats.Count(s => s.Status == SeatStatus.Available);
                TripDetails.arrivalstation = _stationService.GetById(trip.ArrivalStationID).StationName;
                TripDetails.depturestation = _stationService.GetById(trip.DepartureStationID).StationName;
                TripDetails.trip = trip;
                TripDetails.Id = trip.TripId;
                TripDetails.Allseatscount = _busService.GetById(trip.BusId).NumberOfSeats;
                tripdetails.Add(TripDetails);

            }
            var viewModel = new AdminViewModel
            {
                Id = id,
                Buses = buses,
                Drivers = drivers,
                Stations = stations,
                Trips = tripdetails,
                Email = UserEmail

            };

            return View("Admin", viewModel);
        }
        [ServiceFilter(typeof(LoginAuthorizationFilter))]
        [ServiceFilter(typeof(RoleAuthorizationFilter))]
        public IActionResult Dashboard()
        {

            return View();
        }


    }
}
