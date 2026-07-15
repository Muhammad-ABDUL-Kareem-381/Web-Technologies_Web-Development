using FurnitureEmporim.Models.Entities;
using FurnitureEmporim.Models.Repository;
using FurnitureEmporim.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FurnitureEmporim.Controllers
{
    public class ProductsController : Controller
    {
        //IProductRepsitory productDB = new ProductRepository();
        //private readonly IWebHostEnvironment _webHostEnvironment;

        //public ProductsController(IWebHostEnvironment webHostEnvironment)
        //{
        //    _webHostEnvironment = webHostEnvironment;
        //}
        private readonly IProductRepsitory _productDB;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductRepsitory productDB, IWebHostEnvironment webHostEnvironment)
        {
            _productDB = productDB;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var products = _productDB.GetAllProducts();
            return View(products);
        }



        public ActionResult ProductDetail(int id)
        {
            var product = _productDB.GetProductById(id);
            return View(product);
        }
        public ActionResult viewProducts()
        {
            var products = _productDB.GetAllProducts();
            return View(products);

        }




        [HttpGet]
        public ActionResult AddProduct()
        {

            return View("AddProduct");
        }

        [HttpPost]
        public JsonResult AddProduct(Product product)
        {
            var form = Request.Form;

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string imgExtension = Path.GetExtension(file.FileName);
                    fileName = fileName + imgExtension;
                    product.ImageFileName = fileName;
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "products", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }

            bool isAdded = _productDB.AddProduct(product);

            if (isAdded)
            {
                return Json(new { success = true, redirectUrl = Url.Action("AddProduct", "Products") });
            }
            else
            {
                return Json(new { success = false, errorMessage = "Unable to add product to the database." });
            }
        }



        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {

            var product = _productDB.GetProductById(id);

            if (product == null)
            {
                Console.WriteLine("No Product");
                ViewBag.Showmsg = "No Product found with this id";
                return View();
            }



            var existingProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                CreatedAt = product.CreatedAt
            };
            ViewData["ProductID"] = product.Id;
            ViewData["ImageFileName"] = product.ImageFileName;
            return View(existingProduct);
        }



        [HttpPost]
        public ActionResult UpdateProduct(int id, Product modifiedProductData)
        {
            var oldProduct = _productDB.GetProductById(id);
            if (oldProduct == null)
            {
                return RedirectToAction("GuestWelcome", "User");
            }



            string newFileName = oldProduct.ImageFileName;
            if (modifiedProductData.ImageFile != null)
            {
                newFileName = Path.GetFileNameWithoutExtension(modifiedProductData.ImageFile.FileName);
                string imgExtension = Path.GetExtension(modifiedProductData.ImageFile.FileName);
                newFileName = newFileName + imgExtension;
                modifiedProductData.ImageFileName = newFileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "products", newFileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    modifiedProductData.ImageFile.CopyTo(fileStream);
                }


                if (oldProduct.ImageFileName != modifiedProductData.ImageFileName)
                {
                    string oldImageFullPath = Path.Combine(_webHostEnvironment.WebRootPath, oldProduct.ImageFileName);
                    System.IO.File.Delete(oldImageFullPath);

                }
            }

            // Update the product details
            oldProduct.Name = modifiedProductData.Name;
            oldProduct.Price = modifiedProductData.Price;
            oldProduct.Description = modifiedProductData.Description;
            oldProduct.Category = modifiedProductData.Category;
            oldProduct.CreatedAt = modifiedProductData.CreatedAt;
            oldProduct.ImageFileName = newFileName;

            _productDB.UpdateProduct(oldProduct);
            return RedirectToAction("GuestWelcome", "User");
        }


        //public ActionResult DeleteProduct(int id)
        //{

        //    var product = _productDB.GetProductById(id);

        //    if (product == null)
        //    {
        //        ViewBag.Showmsg = "No Product found with this id";
        //        return RedirectToAction("viewProducts", "Products");
        //    }

        //    string imgFullPath = Path.Combine(_webHostEnvironment.WebRootPath, "products", product.ImageFileName);
        //    System.IO.File.Delete(imgFullPath);
        //    _productDB.DeleteProduct(id);
        //    return RedirectToAction("viewProducts", "Products");



        //}

        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            var product = _productDB.GetProductById(id);

            if (product == null)
            {
                return Json(new { success = false, message = "No Product found with this ID." });
            }

            string imgFullPath = Path.Combine(_webHostEnvironment.WebRootPath, "products", product.ImageFileName);

            if (System.IO.File.Exists(imgFullPath))
            {
                System.IO.File.Delete(imgFullPath);
            }

            _productDB.DeleteProduct(id);

            return Json(new { success = true, message = "Product deleted successfully." });
        }


    }
}