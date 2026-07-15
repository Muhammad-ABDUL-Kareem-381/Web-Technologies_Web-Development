using Microsoft.AspNetCore.Mvc;

namespace MediBookClinic.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
