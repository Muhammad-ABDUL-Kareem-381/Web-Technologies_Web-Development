using DoctorConsult_DTO;
using Microsoft.Data.SqlClient;
using System;

namespace DAL
{
    public class DoctorConsultedDAL
    {
        // Fields
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Hospital Management System\";Integrated Security=True;";
        SqlConnection connection;

        // Constructor
        public DoctorConsultedDAL()
        {
            connection = new SqlConnection(connectionString);
        }

        //Methods
        public List<DoctorConsultedDTO> GetMostConsultedDoctors()
        {
            List<DoctorConsultedDTO> result = new List<DoctorConsultedDTO>();
          //string sql = "SELECT TOP 1 WITH TIES d.DoctorID, d.Name, d.Specialization, COUNT(a.AppointmentID) AS AppointmentCount FROM dbo.Doctor d LEFT JOIN dbo.Appointment a ON d.DoctorID = a.DoctorID GROUP BY d.DoctorID, d.Name, d.Specialization ORDER BY AppointmentCount DESC;";
            string sql = "SELECT TOP 1 WITH TIES d.DoctorID, d.Name, d.Specialization, COUNT(a.AppointmentID) AS AppointmentCount FROM Doctor d INNER JOIN Appointment a ON d.DoctorID = a.DoctorID GROUP BY d.DoctorID, d.Name, d.Specialization ORDER BY AppointmentCount DESC";
            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DoctorConsultedDTO doctorConsultedDTO = new DoctorConsultedDTO
                {
                    DoctorID = (int)reader["DoctorID"],
                    Name = (string)reader["Name"],
                    Specialization = (string)reader["Specialization"],
                    AppointmentCount = (int)reader["AppointmentCount"]
                };
                result.Add(doctorConsultedDTO);
            }
            return result;
        }
    }
}
