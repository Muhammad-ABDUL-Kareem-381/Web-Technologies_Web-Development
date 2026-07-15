using SEF23_Web_HW_06.Models.Entities;
using System;

namespace SEF23_Web_HW_06.Models.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts(); // Retrieve all products

        Product GetProduct(int id); // Retrieve a product by its ID
    }
}
