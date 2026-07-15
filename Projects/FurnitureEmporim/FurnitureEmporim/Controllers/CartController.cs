using FurnitureEmporim.Models.Entities;
using FurnitureEmporim.Models.Interface;
using FurnitureEmporim.Models.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureEmporim.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartItem;
        private readonly IProductRepsitory _productDB = new ProductRepository();

        public CartController(ICartRepository cartItem)
        {
            _cartItem = cartItem;
            
        }
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int id)
        {
            Product product = _productDB.GetProductById(id);
            if (product != null)
            {
                var cartList = _cartItem.GetCart();
                var cart = cartList.FirstOrDefault(c => c.product.Id == id);
                if (cart == null)
                {
                    cart = new Cart
                    {
                        product = product,
                        Quantity = 1,
                        SubTotal = product.Price
                    };
                    cartList.Add(cart);
                }
                else
                {
                    cart.Quantity++;
                    cart.SubTotal += product.Price*cart.Quantity;
                }
                _cartItem.saveCart(cartList);
            }
           
            return View("CartView");
        }

        public IActionResult RemoveFromCart(int id)
        {
           
            var cartList = _cartItem.GetCart();

           
            var cartItemToRemove = cartList.FirstOrDefault(c => c.product.Id == id);
            if (cartItemToRemove != null)
            {
                cartList.Remove(cartItemToRemove);

                foreach (var item in cartList)
                {
                    item.SubTotal = item.product.Price * item.Quantity;
                }

                
                _cartItem.saveCart(cartList);
            }

            
            return View("CartView", cartList);
        }



    }
}
