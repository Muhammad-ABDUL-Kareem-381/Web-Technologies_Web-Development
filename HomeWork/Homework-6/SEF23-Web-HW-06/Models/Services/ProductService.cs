using SEF23_Web_HW_06.Models.Entities;
using SEF23_Web_HW_06.Models.Interfaces;
using SEF23_Web_HW_06.Models.Repositories;
using System.Collections.Generic;
namespace SEF23_Web_HW_06.Models.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repo; // Dependency on the product repository

        public ProductService()  // Constructor to initialize the product repository
        {
            repo = new ProductRepository();
        }

        public List<Product> GetProducts() // Retrieve all products
        {
            return repo.GetAllProducts(); // Delegate to the repository
        }

        public Product GetProduct(int id) // Retrieve a product by its ID
        {
            return repo.GetProductById(id); // Delegate to the repository
        }
    }
}

