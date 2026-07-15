using FurnitureEmporim.Models.Interface;
using Microsoft.AspNetCore.Mvc;



namespace FurnitureEmporim.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepsitory _productDB;
        public HomeController(IProductRepsitory productDB)
        {
            _productDB = productDB;
        }
        public  ViewResult Index()
       {
            var products = _productDB.GetAllProducts();
            string userName = HttpContext.Request.Cookies["Username"];
            ViewBag.UserName = userName;

            return View(products);
       }

        public ViewResult About()
        {
            return View();
        }
    }
}
