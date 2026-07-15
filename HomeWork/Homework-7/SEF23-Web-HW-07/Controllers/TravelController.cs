using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEF23_Web_HW_07.Models.Entities;
using SEF23_Web_HW_07.Models.Interfaces;

namespace SEF23_Web_HW_07.Controllers
{
    [Authorize]
    public class TravelController : Controller
    {
        // Private field for the travel service
        private readonly ITravelService travelService;

        // Private field for the user manager
        private readonly UserManager<IdentityUser> userManager;

        // Constructor with Dependency Injection
        public TravelController(ITravelService travelService, UserManager<IdentityUser> userManager)
        {
            this.travelService = travelService;
            this.userManager = userManager;
        }

        // Action method to display all travel packages
        [HttpGet]
        public IActionResult Index()
        {
            return View(travelService.GetAllPackages());
        }

        // Action method to display details of a specific travel package
        [HttpGet]
        public IActionResult Details(int id)
        {
            var package = travelService.GetPackageById(id);
            ViewBag.Package = package;
            return View();
        }

        // Action methods to handle booking a travel package
        [HttpGet]
        public IActionResult Book(int id)
        {
            var package = travelService.GetPackageById(id);
            return View(package);
        }

        // Action method to process the booking form submission
        [HttpPost]
        public IActionResult Book(Booking booking)
        {
            booking.TotalAmount = travelService.GetPackageById(booking.PackageId).Price * booking.NumberOfTravelers;
            booking.UserId = userManager.GetUserId(User);
            bool isBooked = travelService.CreateBooking(booking);
            ViewBag.Booking = isBooked? "Booking Successful!" : "Booking Failed. Please try again.";
            return View("Success");
        }

        // Action method to view user bookings
        [HttpGet]
        public IActionResult MyBooking()
        {
            var userId = userManager.GetUserId(User);
            var bookings = travelService.GetUserBookingsWithDetails(userId);
            return View(bookings);
        }
    }
}
