using Microsoft.AspNetCore.Mvc;

namespace MediBookClinic.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
