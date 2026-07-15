using FurnitureEmporim.Models.Entities;
using FurnitureEmporim.Models.Interface;

namespace FurnitureEmporim.Models.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository()
        {
            Console.WriteLine("Constructor");
        }



        public void PlaceOrder(Order order)
        {
            Console.WriteLine("Place Order");
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            List<Order> orders = new List<Order>();
            return orders;
        }
    }
}

