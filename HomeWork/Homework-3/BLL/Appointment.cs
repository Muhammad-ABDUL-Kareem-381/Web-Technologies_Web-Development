using Appointment_DTO;
using Appointment_DAL;
using System;

namespace Appointment_BLL
{
    public class AppointmentBLL
    {
        public int GenerateAppointmentID()
        {
            var rand = new Random();
            return rand.Next(1, 1000000);
        }
        public void Insert(AppointmentDTO appointmentDTO)
        {
            new AppointmentDAL().Insert(appointmentDTO);
        }
        public void Delete(int appointmentId)
        {
            new AppointmentDAL().Delete(appointmentId);
        }
        public bool Exists(int appointmentId)
        {
            return new AppointmentDAL().Exists(appointmentId);
        }
        public List<AppointmentDTO> GetAll()
        {
            return new AppointmentDAL().GetAll();
        }
        public List<AppointmentDTO> GetByDoctor(int doctorId)
        {
            return new AppointmentDAL().GetByDoctor(doctorId);
        }
        public List<AppointmentDTO> GetByPatient(string cnic)
        {
            return new AppointmentDAL().GetByPatient(cnic);
        }
        public AppointmentDTO GetById(int appointmentId)
        {
            return new AppointmentDAL().GetById(appointmentId);
        }
        public string ToString(AppointmentDTO appointmentDTO)
        {
            if (appointmentDTO == null)
            {
                return string.Empty;
            }
            else
            {
                return $"AppointmentID: {appointmentDTO.AppointmentID}, DoctorID: {appointmentDTO.DoctorID}, PatientCNIC: {appointmentDTO.PatientCNIC}, Date: {appointmentDTO.AppointmentDate}";
            }
        }
    }
}

