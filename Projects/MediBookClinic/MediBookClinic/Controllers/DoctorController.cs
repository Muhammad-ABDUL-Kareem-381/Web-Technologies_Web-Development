using Microsoft.AspNetCore.Mvc;

namespace MediBookClinic.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
