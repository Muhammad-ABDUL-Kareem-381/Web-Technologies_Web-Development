using System;
using Microsoft.AspNetCore.Mvc;
using SEF23_Web_HW_06.Models.Entities;
using System.Text.Json;

namespace SEF23_Web_HW_06.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login() // GET: Login
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Account account) // POST: Login
        {
            if (!string.IsNullOrEmpty(account.Email) && !string.IsNullOrEmpty(account.Password))
            {
                var cradentials = JsonSerializer.Serialize<Account>(account);
                HttpContext.Session.SetString("UserCradentials", cradentials);
                return RedirectToAction("Home");
            }
            ViewBag.Message = "Please enter valid credentials!";
            return View();
        }

        [HttpGet]
        public IActionResult Home() // GET: Home
        {
            if (HttpContext.Session.Keys.Contains("UserCradentials") == false)
            {
                return RedirectToAction("Login");
            }
            var cradentials = HttpContext.Session.GetString("UserCradentials");
            if (string.IsNullOrEmpty(cradentials))
            {
                return RedirectToAction("Login");
            }
            var account = JsonSerializer.Deserialize<Account>(cradentials);
            if (account == null || string.IsNullOrEmpty(account.Email))
            {
                return RedirectToAction("Login");
            }
            ViewBag.UserEmail = account.Email;
            return View();
        }

        [HttpGet]
        public IActionResult Logout() // GET: Logout
        {
            HttpContext.Session.Remove("UserCradentials"); // Remove specific session key
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login"); // Redirect to Login page after logout
        }
    }
}



