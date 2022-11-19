using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.AspNetCore.Http;
using CyoloFrontAppInterface.Models;
using Xamarin.Essentials;

namespace CyoloFrontAppInterface.Controllers
{
    
    public class UserController : Controller
    {
        private readonly ILogger _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userinfo")))
            {
                return RedirectToAction("Index", "CourtCase", new { area = "Manage" });
            }
            ViewData["Message"] = HttpContext.Session.GetString("userinfo");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(User user)
        {
            if (ModelState.IsValid)
            {
                if (await user.IsValid(user.Email!, user.Password!))
                {
                    HttpContext.Session.SetString("userinfo", user.Email!);
                    return RedirectToAction("Index", "CourtCase", new { area = "Manage" });
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            ViewData["error"] = "Login failed. Check your login details.";
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(Register user)
        {
            if (ModelState.IsValid)
            {
                if ( await user.IsValid(user.Email!, user.Password!) )
                {
                    ViewData["error"] = "Registration data already exists";
                    ModelState.AddModelError("", "Registration data already exists!");
                }
                else
                {
                    if (await user.Store(user.Email!, user.Password!))
                    {
                        return RedirectToAction("Login", "User");
                    }
                    ViewData["error"] = "Data registration failed!";
                    ModelState.AddModelError("", "Data registration failed!");
                }
            }
            
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
