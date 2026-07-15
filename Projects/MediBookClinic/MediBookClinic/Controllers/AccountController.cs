using Microsoft.AspNetCore.Mvc;

namespace MediBookClinic.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
