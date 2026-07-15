using FurnitureEmporim.Models.Entities;
using FurnitureEmporim.Models.Interface;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace FurnitureEmporim.Models.Repository
{
    public class ProductRepository : IProductRepsitory
    {
        public string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public ProductRepository()
        {
            Console.WriteLine("COnstructor");
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Products";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Description = reader["Description"].ToString(),
                        Category = reader["Category"].ToString(),
                        ImageFileName = reader["ImageFileName"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])

                    };
                    products.Add(product);
                }
                reader.Close();
            }


            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string query = "SELECT * FROM Products WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Description = reader["Description"].ToString(),
                        Category = reader["Category"].ToString(),
                        ImageFileName = reader["imageFileName"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    };
                }
                reader.Close();
            }
            return product;
        }

        public bool AddProduct(Product product)
        {
            string query = "INSERT INTO Products (Name, Price, Description, Category, ImageFileName, CreatedAt) VALUES (@Name, @Price, @Description, @Category, @ImageFileName, @CreatedAt)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Category", product.Category);
                    command.Parameters.AddWithValue("@ImageFileName", product.ImageFileName);
                    command.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);

                    conn.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (using a logging framework or a file)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }


        public void UpdateProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Products SET Name = @Name, Price = @Price, Description = @Description, Category = @Category, ImageFileName = @ImageFileName, CreatedAt = @CreatedAt  WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("Description", product.Description);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@ImageFileName", product.ImageFileName);
                command.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);
                int result = command.ExecuteNonQuery();
                Console.WriteLine("Update Product");
                
            }
        }
            public void DeleteProduct(int id)
            {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Products WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                int result = command.ExecuteNonQuery();
                Console.WriteLine("Delete Product");
                
            }
           
            }
        }
    }


