
using FurnitureEmporim.Models.Entities;
using FurnitureEmporim.Models.Repository;
using FurnitureEmporim.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace FurnitureEmporim.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return View("Index");
            }

            var row = _userRepository.GetAllUsers().Where(model => model.Email == email).FirstOrDefault();
            return View(row);
        }

        [HttpPost]
        public ActionResult Index(Userss um, string submitbutton)
        {
            if (um == null || string.IsNullOrEmpty(um.Email) || string.IsNullOrEmpty(um.Password))
            {
                ViewBag.Showmsg = "Email and Password are required.";
                return View(um);
            }

            string adminEmail = "iamadmin111@gmail.com";
            string adminPassword = "iamadmin";

            try
            {
                if (submitbutton == "Guest User")
                {
                    return View(um);
                }
                else
                {
                    var data = _userRepository.GetUserByEmail(um.Email);

                   
                    if (data != null)
                    {
                        //Logout();
                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddDays(1);

                        if (!string.IsNullOrEmpty(um.Email))
                        {
                            HttpContext.Response.Cookies.Append("Email", um.Email, option);
                            HttpContext.Session.SetString("EmailSession", um.Email);
                        }

                       
                            HttpContext.Session.SetString("UserNameSession", data.Username);
                            Console.WriteLine("Set USername session in Login");
                        





                        return RedirectToAction("Index", "Home");
                    }
                    else if (um.Email == adminEmail && um.Password == adminPassword)
                    {
                        return View("GuestWelcome", um);
                    }
                    else
                    {
                        ViewBag.Showmsg = "Invalid user ID or Password";
                        return View(um);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Showmsg = $"An error occurred: {ex.Message}";
                return View(um);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Userss um)
        {
            if (um == null || string.IsNullOrEmpty(um.Email) || string.IsNullOrEmpty(um.Username) || string.IsNullOrEmpty(um.Password))
            {
                ViewBag.Showmsg = "All fields are required.";
                return View(um);
            }
              

            try
            {
                bool isAdded = _userRepository.AddUser(um);

                if (isAdded)
                {
                    //Logout();
                    CookieOptions option = new CookieOptions { Expires = DateTime.Now.AddDays(1) };

                    if (!string.IsNullOrEmpty(um.Username))
                    {
                        HttpContext.Response.Cookies.Append("Username", um.Username, option);
                        HttpContext.Session.SetString("UserNameSession", um.Username);
                        Console.WriteLine("Set Session of Username in sign up");
                    }

                    if (!string.IsNullOrEmpty(um.Email))
                    {
                        HttpContext.Response.Cookies.Append("Email", um.Email, option);

                        HttpContext.Session.SetString("EmailSession", um.Email);
                        Console.WriteLine("Set session of Email in signup");
                    }
                        

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ViewBag.Showmsg = "Failed to register the user.";
                    return View(um);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Showmsg = $"An error occurred: {ex.Message}";
                return View(um);
            }
        }

        public ActionResult GuestWelcome()
        {
            return View();  // Fixing the redirection loop issue
        }

        public ActionResult Logout()
        {
            // Clear the user cookies on logout
            //HttpContext.Response.Clear();
            //HttpContext.Response.Cookies.Delete("Email");
            //HttpContext.Response.Cookies.Delete("Username");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "User");
        }
    }
}

