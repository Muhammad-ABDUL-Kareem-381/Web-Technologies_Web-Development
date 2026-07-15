using Appointment_BLL;
using Appointment_DTO;
using DAL;
using Doctor_BLL;
using Doctor_DTO;
using DoctorConsult_DTO;
using HospitalSystem_DTO;
using Logger;
using Patient_BLL;
using Patient_DTO;
using System;

namespace HospitalSystem_BLL
{
    public class HospitalSystemBLL
    {
        // Fields
        private Logging logging = new Logging();
        private DoctorBLL doctorBLL = new DoctorBLL();
        private PatientBLL patientBLL = new PatientBLL();
        private AppointmentBLL appointmentBLL = new AppointmentBLL();

        // Constructor
        public HospitalSystemBLL()
        {
            
        }

        // Public Methods
        public void AddPatient(string name, string cnic)
        {
            if (string.IsNullOrEmpty(cnic) || string.IsNullOrWhiteSpace(cnic) || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return;
            }
            else if (patientBLL.Exists(cnic))
            {
                return;
            }
            else
            {
                PatientDTO temp = new PatientDTO(name, cnic);
                patientBLL.Insert(temp);
                logging.LogPatient("Added: ", temp);
            }
        }

        public void UpdatePatient(string name, string cnic)
        {
            if (string.IsNullOrEmpty(cnic) || string.IsNullOrWhiteSpace(cnic) || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return;
            }
            else if (patientBLL.Exists(cnic))
            {
                PatientDTO temp = new PatientDTO(name, cnic);
                patientBLL.Update(temp);
                logging.LogPatient("Updated: ", temp);
            }
            else
            {
                return;
            }
        }

        public void DeletePatient(string cnic)
        {
            if (string.IsNullOrEmpty(cnic) || string.IsNullOrWhiteSpace(cnic))
            {
                return;
            }
            else if (patientBLL.Exists(cnic))
            {
                logging.LogPatient("Deleted: ", patientBLL.GetByCnic(cnic));
                patientBLL.Delete(cnic);
            }
            else
            {
                return;
            }
        }

        public List<PatientDTO> DisplayPatients()
        {
            return patientBLL.GetAll();
        }

        public void AddDoctor(string name, string specialization)
        {
            if (string.IsNullOrEmpty(specialization) || string.IsNullOrWhiteSpace(specialization) || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return;
            }
            int id = doctorBLL.GenerateDoctorID();
            while (doctorBLL.Exists(id))
            {
                id = doctorBLL.GenerateDoctorID();
            }
            DoctorDTO temp = new DoctorDTO(id,name,specialization);
            doctorBLL.Insert(temp);
            logging.LogDoctor("Added: ", temp);
        }

        public void UpdateDoctor(int doctorID, string Name, string Specialization)
        {
            if (doctorID <= 0)
            {
                return;
            }
            else if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrEmpty(Specialization) || string.IsNullOrWhiteSpace(Specialization))
            {
                return;
            }
            else if (doctorBLL.Exists(doctorID))
            {
                DoctorDTO temp = new DoctorDTO(doctorID, Name, Specialization);
                doctorBLL.Update(temp);
                logging.LogDoctor("Updated: ",temp);
            }
            else
            {
                return;   
            }
        }

        public void DeleteDoctor(int doctorID)
        {
            if (doctorID <= 0)
            {
                return;
            }
            else if (doctorBLL.Exists(doctorID))
            {
                logging.LogDoctor("Deleted: ", doctorBLL.GetById(doctorID));
                doctorBLL.Delete(doctorID);
            }
            else
            {
                return;
            }

        }

        public List<DoctorDTO> DisplayDoctors()
        {
            return doctorBLL.GetAll();
        }

        public void BookAppointment(int doctorID, string cnic, DateTime date)
        {
            if (doctorID <= 0 || string.IsNullOrEmpty(cnic) || string.IsNullOrWhiteSpace(cnic) || date <= DateTime.Now)
            {
                return;
            }
            else if (!doctorBLL.Exists(doctorID))
            {
                return;
            }
            else if (!patientBLL.Exists(cnic))
            {
                return;
            }
            DoctorDTO doctor = doctorBLL.GetById(doctorID);
            if (!doctor.IsAvailable)
            {
                return;
            }
            int id = appointmentBLL.GenerateAppointmentID();
            while (appointmentBLL.Exists(id))
            {
                id = appointmentBLL.GenerateAppointmentID();
            }
            AppointmentDTO appointmentDTO = new AppointmentDTO(id,doctorID,cnic,date);
            appointmentBLL.Insert(appointmentDTO);
            logging.LogAppointment("Booked: ",appointmentDTO);
        }

        public void CancelAppointment(int appointmentID, string cnic)
        {
            if (appointmentID <= 0 || string.IsNullOrEmpty(cnic) || string.IsNullOrWhiteSpace(cnic))
            {
                return;
            }
            else if (!appointmentBLL.Exists(appointmentID))
            {
                return;
            }
            AppointmentDTO appointmentDTO = appointmentBLL.GetById(appointmentID);
            if (!(appointmentDTO.PatientCNIC == cnic))
            {
                return;
            }
            appointmentBLL.Delete(appointmentID);
            logging.LogAppointment("Cancelled: ", appointmentDTO);
        }

        public List<DoctorConsultedDTO> GetMostConsultedDoctors()
        {
            return new DoctorConsultedDAL().GetMostConsultedDoctors();
        }
    }
}
