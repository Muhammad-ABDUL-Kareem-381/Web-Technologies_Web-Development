using FurnitureEmporim.Models.Entities;

namespace FurnitureEmporim.Models.Interface
{
    public interface IProductRepsitory
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        bool AddProduct(Product product);
         void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
