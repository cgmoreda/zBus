using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using zBus.Models;

namespace zBus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       
        public IActionResult Index()
        {
            ViewBag.ActivePage = "Home";
            return View();
        }

        public IActionResult LoginReminder()
        {
            return PartialView("LoginReminder");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.ActivePage = "About";
            return View();
        }


        public IActionResult Contact()
        {
            ViewBag.ActivePage = "Contact";
            return View();
        }

        public IActionResult Services()
        {
            ViewBag.ActivePage = "Services";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}