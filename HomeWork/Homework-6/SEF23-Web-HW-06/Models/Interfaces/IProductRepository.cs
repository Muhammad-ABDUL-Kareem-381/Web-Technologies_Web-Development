using System;
using SEF23_Web_HW_06.Models.Entities;
using System.Collections.Generic;

namespace SEF23_Web_HW_06.Models.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts(); // Retrieve all products

        Product GetProductById(int id); // Retrieve a product by its ID
    }
}

