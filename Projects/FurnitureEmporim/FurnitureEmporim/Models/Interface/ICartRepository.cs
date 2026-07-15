
using FurnitureEmporim.Models.Entities;

namespace FurnitureEmporim.Models.Interface
{
    public interface ICartRepository
    {
        public List<Cart>? GetCart();
        public void saveCart(List<Cart> cart);
    }
}
