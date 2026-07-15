using System;

namespace DoctorConsult_DTO
{
    public class DoctorConsultedDTO
    {
        // Fields
        private int doctorID;
        private string? name;
        private string? specialization;
        private int appointmentCount;

        // Constructor
        public DoctorConsultedDTO()
        {
            
        }

        // Properties
        public int DoctorID
        {
            get { return doctorID; }
            set { doctorID = value; }
        }
        public string? Name
        {
            get { return name; }
            set { name = value; }
        }
        public string? Specialization
        {
            get { return specialization; }
            set { specialization = value; }
        }
        public int AppointmentCount
        {
            get { return appointmentCount; }
            set { appointmentCount = value; }
        }
    }
}

