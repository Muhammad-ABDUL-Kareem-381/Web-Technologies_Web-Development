using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FurnitureEmporim.Models.Entities;
using Microsoft.AspNetCore.Http;
using FurnitureEmporim.Models.Interface;


namespace FurnitureEmporim.Models.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<Cart>? GetCart()
        {
            var cartJson = _httpContextAccessor.HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
                return new List<Cart>();

            return JsonSerializer.Deserialize<List<Cart>>(cartJson);
        }

        public void saveCart(List<Cart> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            _httpContextAccessor.HttpContext.Session.SetString("Cart", cartJson);
        }
    }
}
