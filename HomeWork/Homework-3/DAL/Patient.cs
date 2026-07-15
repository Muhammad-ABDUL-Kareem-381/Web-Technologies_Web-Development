using System;
using Microsoft.Data.SqlClient;
using Patient_DTO;

namespace Patient_DAL
{
    public class PatientDAL
    {
        // Connection String
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Hospital Management System\";Integrated Security=True;";

        // Constructor
        public PatientDAL()
        {
    
        }

        // Methods
        public void Insert(PatientDTO patientDTO)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Patient (Name, CNIC) VALUES (@name, @cnic)", conn);
            cmd.Parameters.AddWithValue("@name", patientDTO.Name);
            cmd.Parameters.AddWithValue("@cnic", patientDTO.CNIC);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void Update(PatientDTO patientDTO)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Patient SET Name = @name WHERE CNIC = @cnic", conn);
            cmd.Parameters.AddWithValue("@name", patientDTO.Name);
            cmd.Parameters.AddWithValue("@cnic", patientDTO.CNIC);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void Delete(string cnic)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Patient WHERE CNIC = @cnic", conn);
            cmd.Parameters.AddWithValue("@cnic", cnic);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public bool Exists(string cnic)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Patient WHERE CNIC = @cnic", conn);
            cmd.Parameters.AddWithValue("@cnic", cnic);
            conn.Open();
            int result = (int)cmd.ExecuteScalar();
            conn.Close();
            return result > 0;
        }
        public List<PatientDTO> GetAll()
        {
            List<PatientDTO> patients = new List<PatientDTO>();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Name, CNIC FROM Patient", conn);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string? name = rdr.GetString(0);
                string? cnic = rdr.GetString(1);
                patients.Add(new PatientDTO(name, cnic));
            }
            conn.Close();
            return patients;
        }
        public PatientDTO GetByCnic(string cnic)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Name, CNIC FROM Patient WHERE CNIC = @cnic", conn);
            cmd.Parameters.AddWithValue("@cnic", cnic);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                string? name = rdr.GetString(0);
                string? foundCnic = rdr.GetString(1);
                conn.Close();
                return new PatientDTO(name, foundCnic);
            }
            conn.Close();
            return new PatientDTO();
        }
    }
}
