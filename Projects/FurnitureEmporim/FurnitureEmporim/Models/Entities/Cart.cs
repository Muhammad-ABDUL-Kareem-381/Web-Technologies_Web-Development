namespace FurnitureEmporim.Models.Entities
{
    public class Cart
    {
        public Product? product { get; set; } = null;
        public int Quantity { get; set; } = -1;
        public decimal SubTotal { get; set; } = 0; 
    }
}
