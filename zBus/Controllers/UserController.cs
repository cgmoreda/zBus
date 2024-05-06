using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using zBus.Data.Services;
using zBus.GLobal;
using zBus.Models;

namespace zBus.Controllers
{
    public class UserController : Controller
    {
        
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login_Page() {

            return View();
        }

        public IActionResult Register()
        {

            return View(new User());

        }
        public IActionResult Register_Save(User user)
         {

            if (ModelState.IsValid)
            {
      
                    if (_userService.Exist(user.Email))
                    {
                        ModelState.AddModelError("email", "This Email is already registered.");
                        return View("Register", user);
                    }
                    else
                    {
                        _userService.Add(user);
                        return View("Login_Page");
                    }
                }
           
            
            return View("Register", user);
         }
        

        public IActionResult Login_Valid(string email, string Password) {
            ViewBag.valid=string.Empty;
            if (ModelState.IsValid)
            {
                if (_userService.Exist(email))
                {
                    var user = _userService.GetById(email);
                    if (user.Password == Password)
                    {
                        GlobalVariables.Login_Status=true;
                        GlobalVariables.User= user.Email;
                        return RedirectToAction("Index", "Home");
                    }
                  
                    {
                        ModelState.AddModelError("password","Password is not Correct");
                        return View("Login_Page");
                    }
                }
                else
                {
                    ModelState.AddModelError("email","Email does not exist try a valid one");
                    return View("Login_Page");
                }
            }

            return View("Login_Page");

        }

        public IActionResult Logout() {

            GlobalVariables.Login_Status = false;
            GlobalVariables.User = String.Empty;
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Account() {

            var user = _userService.GetById(GlobalVariables.User!);
            return View(user);
        }



        public IActionResult Delete()
        {
            _userService.Delete(GlobalVariables.User!);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Update_save(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.Update(GlobalVariables.User!, user);
                return RedirectToAction("Index", "Home");
            }

            return View("Account", user);
        }

        public IActionResult Update_Password(string Password)
        {
            if (ModelState.IsValid)
            {
                _userService.Update_Pass(Password);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Book()
        {

            return View();
        }

        public IActionResult Admin()
        {

            return View();
        }
        public IActionResult Dashboard()
        {

            return View();
        }


    }
}
