using System;
using Microsoft.Data.SqlClient;
using Appointment_DTO;

namespace Appointment_DAL
{
    public class AppointmentDAL
    {
        // Connection String
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Hospital Management System\";Integrated Security=True;";

        // Condtructor
        public AppointmentDAL()
        {
        
        }

        // Methods
        public void Insert(AppointmentDTO appointmentDTO)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Appointment (AppointmentID, DoctorID, PatientCNIC, AppointmentDate) VALUES (@id, @docId, @cnic, @date)", conn);
            cmd.Parameters.AddWithValue("@id", appointmentDTO.AppointmentID);
            cmd.Parameters.AddWithValue("@docId", appointmentDTO.DoctorID);
            cmd.Parameters.AddWithValue("@cnic", appointmentDTO.PatientCNIC);
            cmd.Parameters.AddWithValue("@date", appointmentDTO.AppointmentDate);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void Delete(int appointmentId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Appointment WHERE AppointmentID = @id", conn);
            cmd.Parameters.AddWithValue("@id", appointmentId);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public bool Exists(int appointmentId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Appointment WHERE AppointmentID = @id", conn);
            cmd.Parameters.AddWithValue("@id", appointmentId);
            conn.Open();
            int result = (int)cmd.ExecuteScalar();
            conn.Close();
            return result > 0;
        }
        public List<AppointmentDTO> GetAll()
        {
            List<AppointmentDTO> list = new List<AppointmentDTO>();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT AppointmentID, DoctorID, PatientCNIC, AppointmentDate FROM Appointment", conn);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int apId = rdr.GetInt32(0);
                int docId = rdr.GetInt32(1);
                string? cnic = rdr.GetString(2);
                DateTime date = rdr.GetDateTime(3);
                list.Add(new AppointmentDTO(apId, docId, cnic, date));
            }
            conn.Close();
            return list;
        }
        public List<AppointmentDTO> GetByDoctor(int doctorId)
        {
            List<AppointmentDTO> list = new List<AppointmentDTO>();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT AppointmentID, DoctorID, PatientCNIC, AppointmentDate FROM Appointment WHERE DoctorID = @docId", conn);
            cmd.Parameters.AddWithValue("@docId", doctorId);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int apId = rdr.GetInt32(0);
                int docId = rdr.GetInt32(1);
                string? cnic = rdr.GetString(2);
                DateTime date = rdr.GetDateTime(3);
                list.Add(new AppointmentDTO(apId, docId, cnic, date));
            }
            conn.Close();
            return list;
        }
        public List<AppointmentDTO> GetByPatient(string cnic)
        {
            List<AppointmentDTO> list = new List<AppointmentDTO>();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT AppointmentID, DoctorID, PatientCNIC, AppointmentDate FROM Appointment WHERE PatientCNIC = @cnic", conn);
            cmd.Parameters.AddWithValue("@cnic", cnic);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int apId = rdr.GetInt32(0);
                int docId = rdr.GetInt32(1);
                string? foundCnic = rdr.GetString(2);
                DateTime date = rdr.GetDateTime(3);
                list.Add(new AppointmentDTO(apId, docId, foundCnic, date));
            }
            conn.Close();
            return list;
        }
        public AppointmentDTO GetById(int appointmentId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT AppointmentID, DoctorID, PatientCNIC, AppointmentDate FROM Appointment WHERE AppointmentID = @id", conn);
            cmd.Parameters.AddWithValue("@id", appointmentId);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                int apId = rdr.GetInt32(0);
                int docId = rdr.GetInt32(1);
                string? cnic = rdr.GetString(2);
                DateTime date = rdr.GetDateTime(3);
                return new AppointmentDTO(apId, docId, cnic, date);
            }
            conn.Close();
            return new AppointmentDTO();
        }
    }
}
