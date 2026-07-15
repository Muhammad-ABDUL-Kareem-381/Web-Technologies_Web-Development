using System;

namespace Patient_DTO
{
    public class PatientDTO
    {
        // Fields
        private string? name;
        private string? cnic;
        private List<int> appointments;

        // Constructors
        public PatientDTO()
        {
            this.name = null;
            this.cnic = null;
            this.appointments = new List<int>();
        }
        public PatientDTO(string? name, string? cnic)
        {
            this.name = name;
            this.cnic = cnic;
            this.appointments = new List<int>();
        }
        public PatientDTO(string? name, string? cnic, List<int> appointments)
        {
            this.name = name;
            this.cnic = cnic;
            this.appointments = appointments;
        }

        // Properties
        public string? Name
        {
            get { return name; }
            set { name = value; }
        }
        public string? CNIC
        {
            get { return cnic; }
        }
        public List<int> Appointments
        {
            set { appointments = value; }
            get { return appointments; }
        }
    }
}

