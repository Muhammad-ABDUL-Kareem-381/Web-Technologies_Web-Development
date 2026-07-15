using FurnitureEmporim.Models.Entities;

namespace FurnitureEmporim.Models.Interface
{
    public interface IOrderRepository
    {
        void PlaceOrder(Order order);
        List<Order> GetOrdersByUserId(int userId);
    }
}
