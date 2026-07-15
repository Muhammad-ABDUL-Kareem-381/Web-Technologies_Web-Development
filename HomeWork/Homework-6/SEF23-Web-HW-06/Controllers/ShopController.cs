using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SEF23_Web_HW_06.Models.Entities;
using SEF23_Web_HW_06.Models.Interfaces;
using SEF23_Web_HW_06.Models.Services;


namespace SEF23_Web_HW_06.Controllers // Namespace for the ShopController
{
    public class ShopController : Controller // Controller for managing the shopping functionalities
    {
        private readonly IProductService productService; // Dependency on the product service

        public ShopController() // Constructor to initialize the product service
        {
            productService = new ProductService(); // Instantiate the product service
        }

        [HttpGet]
        public IActionResult ProductList() // Action to display the list of products
        {
            var products = productService.GetProducts(); // Retrieve all products from the service
            return View(products); // Return the view with the list of products
        }

        [HttpGet]
        public IActionResult AddToCart(int id) // Action to add a product to the shopping cart
        {
            var product = productService.GetProduct(id); // Retrieve the product by its ID
            if (product == null) // If the product is not found
            {
                TempData["msg"] = "Product not found!";
                return RedirectToAction("ProductList");
            }
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(2);
            TempData["msg"] = $"Product: {product.Name} added to cart successfully!";
            if (HttpContext.Request.Cookies.ContainsKey("UserCart")) // Check if the UserCart cookie exists
            {
                string? existingCart = HttpContext.Request.Cookies["UserCart"];
                if (!string.IsNullOrEmpty(existingCart)) // If the existingcart is not empty
                {
                    var lists = JsonSerializer.Deserialize<List<Product>>(existingCart);
                    lists?.Add(product);
                    var updatedCart = JsonSerializer.Serialize(lists);
                    HttpContext.Response.Cookies.Append("UserCart", updatedCart,cookieOptions);
                    return RedirectToAction("ProductList");
                }
                else // If the existing cart is empty
                {
                    List<Product> cartItems = new List<Product>();
                    cartItems.Add(product);
                    var cartData = JsonSerializer.Serialize(cartItems);
                    HttpContext.Response.Cookies.Append("UserCart", cartData, cookieOptions);
                    return RedirectToAction("ProductList");
                }
            }
            else // If the UserCart cookie does not exist
            {
                List<Product> cartItems = new List<Product>();
                cartItems.Add(product);
                var cartData = JsonSerializer.Serialize(cartItems);
                HttpContext.Response.Cookies.Append("UserCart", cartData, cookieOptions);
                return RedirectToAction("ProductList");
            }
        }

        [HttpGet]
        public IActionResult Cart() // Action to display the shopping cart
        {
            if (!HttpContext.Request.Cookies.ContainsKey("UserCart"))  // If the UserCart cookie does not exist
            {
                return View(new List<CartItems>());
            }
            var cookie = HttpContext.Request.Cookies["UserCart"];
            List<CartItems> cartItems = new List<CartItems>();
            if (!string.IsNullOrEmpty(cookie)) // If the cookie is not empty
            {
                var products = JsonSerializer.Deserialize<List<Product>>(cookie);
                foreach(var product in products)
                {
                    bool present = false;
                    foreach (var cartitem in cartItems)
                    {
                        if (cartitem.product.Id == product.Id)
                        {
                            cartitem.Quantity++;
                            present = true;
                        }
                    }
                    if (!present)
                    {
                        cartItems.Add(new CartItems() {product = product, Quantity = 1 });
                    }
                }
            }
            return View(cartItems);
        }
        
        [HttpGet]
        public IActionResult ClearCart() // Action to clear the shopping cart
        {
            if (HttpContext.Request.Cookies.ContainsKey("UserCart")) // If the UserCart cookie exists
            {
                HttpContext.Response.Cookies.Delete("UserCart"); // Delete the UserCart cookie
            }
            return RedirectToAction("Cart");
        }
    }
}

