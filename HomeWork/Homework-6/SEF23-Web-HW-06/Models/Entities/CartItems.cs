using System;

namespace SEF23_Web_HW_06.Models.Entities
{
    public class CartItems
    {
        public Product? product { get; set; } // Product in the cart

        public int Quantity { get; set; } // Quantity of the product in the cart
    }
}
