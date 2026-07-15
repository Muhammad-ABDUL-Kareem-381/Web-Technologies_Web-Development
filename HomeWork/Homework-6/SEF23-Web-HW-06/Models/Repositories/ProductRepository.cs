using System;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using SEF23_Web_HW_06.Models.Entities;
using SEF23_Web_HW_06.Models.Interfaces;

namespace SEF23_Web_HW_06.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Online Shopping Website\";Integrated Security=True;";
        SqlConnection conn;

        public ProductRepository() // Constructor to initialize the SQL connection
        {
            conn = new SqlConnection(connectionString);
        }

        public List<Product> GetAllProducts() // Retrieve all products from the database
        {
            conn.Open();
            string query = "SELECT * FROM Products";
            var products = conn.Query<Product>(query).ToList();
            return products;
        }

        public Product GetProductById(int id) // Retrieve a product by its ID from the database
        {
            conn.Open();
            string query = "SELECT * FROM Products WHERE Id = @Id";
            var product = conn.QueryFirstOrDefault<Product>(query, new { Id = id });
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }
    }
}
