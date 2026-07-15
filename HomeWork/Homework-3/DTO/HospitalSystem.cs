using System;
using Doctor_DTO;
using Patient_DTO;
using Appointment_DTO;

namespace HospitalSystem_DTO
{
    public class HospitalSystemDTO
    {
        // Fields
        private List<DoctorDTO>? doctors;
        private List<PatientDTO>? patients;
        private List<AppointmentDTO>? appointments;

        // Constructor
        public HospitalSystemDTO()
        {

        }
        public HospitalSystemDTO(List<DoctorDTO> doctors, List<PatientDTO> patients, List<AppointmentDTO> appointments)
        {
            this.doctors = doctors;
            this.patients = patients;
            this.appointments = appointments;
        }

        // Properties
        public List<DoctorDTO>? Doctors
        {
            get { return doctors; }
            set { doctors = value; }
        }
        public List<PatientDTO>? Patients
        {
            get { return patients; }
            set { patients = value; }
        }
        public List<AppointmentDTO>? Appointments
        {
            get { return appointments; }
            set { appointments = value; }
        }
    }
}
