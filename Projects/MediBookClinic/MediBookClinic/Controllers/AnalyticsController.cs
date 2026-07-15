using Microsoft.AspNetCore.Mvc;

namespace MediBookClinic.Controllers
{
    public class AnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
