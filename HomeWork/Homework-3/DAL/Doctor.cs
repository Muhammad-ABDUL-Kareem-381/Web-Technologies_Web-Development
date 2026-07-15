using System;
using Microsoft.Data.SqlClient;
using Doctor_DTO;

namespace Doctor_DAL
{
    public class DoctorDAL
    {
        // Connection String
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Hospital Management System\";Integrated Security=True;";
        
        // Constructor
        public DoctorDAL()
        {

        }

        // Methods
        public void Insert(DoctorDTO doctorDTO)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Doctor (DoctorID, Name, Specialization, IsAvailable) VALUES (@id, @name, @spec, @avail)", conn);
            cmd.Parameters.AddWithValue("@id", doctorDTO.DoctorID);
            if (doctorDTO.Name == null)
            {
                cmd.Parameters.AddWithValue("@name", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@name", doctorDTO.Name);
            }
            if (doctorDTO.Specialization == null)
            {
                cmd.Parameters.AddWithValue("@spec", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@spec", doctorDTO.Specialization);
            }
            cmd.Parameters.AddWithValue("@avail", doctorDTO.IsAvailable);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void Update(DoctorDTO doctorDTO)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Doctor SET Name = @name, Specialization = @spec, IsAvailable = @avail WHERE DoctorID = @id", conn);
            if (doctorDTO.Name == null)
            {
                cmd.Parameters.AddWithValue("@name", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@name", doctorDTO.Name);
            }
            if (doctorDTO.Specialization == null)
            {
                cmd.Parameters.AddWithValue("@spec", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@spec", doctorDTO.Specialization);
            }
            cmd.Parameters.AddWithValue("@avail", doctorDTO.IsAvailable);
            cmd.Parameters.AddWithValue("@id", doctorDTO.DoctorID);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void Delete(int doctorId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Doctor WHERE DoctorID = @id", conn);
            cmd.Parameters.AddWithValue("@id", doctorId);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public bool Exists(int doctorId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Doctor WHERE DoctorID = @id", conn);
            cmd.Parameters.AddWithValue("@id", doctorId);
            conn.Open();
            int result = (int)cmd.ExecuteScalar();
            conn.Close();
            return result > 0;
        }
        public List<DoctorDTO> GetAll()
        {
            List<DoctorDTO> doctors = new List<DoctorDTO>();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Doctor", conn);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string spec = rdr.GetString(2);
                bool avail = rdr.GetBoolean(3);

                doctors.Add(new DoctorDTO(id, name, spec, avail));
            }
            conn.Close();
            return doctors;
        }
        public DoctorDTO GetById(int doctorId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Doctor WHERE DoctorID = @id", conn);
            cmd.Parameters.AddWithValue("@id", doctorId);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string? name = rdr.GetString(1);
                string? spec = rdr.GetString(2);
                bool avail = rdr.GetBoolean(3);
                conn.Close();
                return new DoctorDTO(id, name, spec, avail);
            }
            conn.Close();
            return new DoctorDTO();
        }
    }
}