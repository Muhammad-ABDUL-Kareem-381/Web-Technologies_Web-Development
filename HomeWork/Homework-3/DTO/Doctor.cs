using System;

namespace Doctor_DTO
{
    public class DoctorDTO
    {
        // Fields
        private readonly int doctorID;
        private string name;
        private string specialization;
        private bool isAvailable;

        // Constructors
        public DoctorDTO()
        {
            this.doctorID = -1; // Default Constructor
            this.name = "";
            this.specialization = "";
            this.isAvailable = true;
        }
        public DoctorDTO(int doctorID, string name, string specialization) // Parametrized Constructor
        {
            this.doctorID = doctorID;
            this.name = name;
            this.specialization = specialization;
            this.isAvailable = true;
        }
        public DoctorDTO(int doctorID, string name, string specialization, bool isAvailable) // For loading from DB
        {
            this.doctorID = doctorID;
            this.name = name;
            this.specialization = specialization;
            this.isAvailable = isAvailable;
        }

        // Properties
        public int DoctorID
        {
            get { return doctorID; }
        }
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        public string Specialization
        {
            set { specialization = value; }
            get { return specialization; }
        }
        public bool IsAvailable
        {
            get { return this.isAvailable; }
            set { this.isAvailable = value; }
        }
    }
}

