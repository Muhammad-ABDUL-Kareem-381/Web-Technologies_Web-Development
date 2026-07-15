using Microsoft.Data.SqlClient;
using FurnitureEmporim.Models.Entities;
using FurnitureEmporim.Models.Interface;
using System.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace FurnitureEmporim.Models.Repository
{


    public class UserRepository : IUserRepository
    {
        public string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public bool AddUser(Userss userss)
        {
            string query = "INSERT INTO Users (emailID, Username, password) VALUES ( @Email, @Username, @Password )";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            command.Parameters.AddWithValue("@Email", userss.Email);
            command.Parameters.AddWithValue("@Username", userss.Username);
            command.Parameters.AddWithValue("@Password", userss.Password);


            int a = command.ExecuteNonQuery();
            if (a >= 1)
            {
                return true;
            }
            else
            { return false; }
        }
        public void UpdateUser(Userss userss)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Users SET emailID = @emailID, Username = @Username, password = @password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@emailID", userss.Email);
                command.Parameters.AddWithValue("@Username", userss.Username);
                command.Parameters.AddWithValue("@password", userss.Password);
                command.ExecuteNonQuery();
                Console.WriteLine("Update Product");
            }


        }
        public void DeleteUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Users WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
            Console.WriteLine("Delete Product");

        }

        public List<Userss> GetAllUsers()
        {
            List<Userss> lst = new List<Userss>();
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "select * from Users";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            ad.Fill(dataTable);
            foreach (DataRow us in dataTable.Rows)
            {
                lst.Add(new Userss
                {
                    ID = Convert.ToInt32(us["Id"]),
                    Email = us["emailID"].ToString(),
                    Username = us["Username"].ToString(),
                    Password = us["password"].ToString(),

                });
            }
            return lst;
        }

        public Userss GetUserByEmail(string email)
        {
            Userss users = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string query = "SELECT * FROM Users WHERE emailID = @emailID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@emailID", email);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    users = new Userss
                    {
                        ID = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Email = reader["emailID"].ToString(),
                        Password = reader["password"].ToString()

                    };
                }
                reader.Close();
            }
            return users;

        }

        public bool isUSerExist(string email, string passwd)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT COUNT(1) FROM Users WHERE emailID = @emailID AND password = @password";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@emailID", email);
                    cmd.Parameters.AddWithValue("@password", passwd);

                    int userCount = (int)cmd.ExecuteScalar();

                    return userCount > 0;
                }
            }
        }
    }
    }

