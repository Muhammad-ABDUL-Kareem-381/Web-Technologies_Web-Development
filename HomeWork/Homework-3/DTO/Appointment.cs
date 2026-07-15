using System;

namespace Appointment_DTO
{
    public class AppointmentDTO
    {
        // Fields
        private readonly int appointmentID;
        private int doctorID;
        private string? patientCNIC;
        private DateTime appointmentDate;

        // Constructors
        public AppointmentDTO()
        {
            this.appointmentID = 0;
            this.doctorID = 0;
            this.patientCNIC = null;
            this.appointmentDate = new DateTime();
        }
        public AppointmentDTO(int appointmentID, int doctorID, string patientCNIC, DateTime appointmentDate)
        {
            this.appointmentID = appointmentID;
            this.doctorID = doctorID;
            this.patientCNIC = patientCNIC;
            this.appointmentDate = appointmentDate;
        }

        // Properties
        public int AppointmentID
        {
            get { return appointmentID; }
        }
        public int DoctorID
        {
            set { doctorID = value; }
            get { return doctorID; }
        }
        public string? PatientCNIC
        {
            set { patientCNIC = value; }
            get { return patientCNIC; }
        }
        public DateTime AppointmentDate
        {
            set { appointmentDate = value; }
            get { return appointmentDate; }
        }
    }
}

