using Appointment_DTO;
using Doctor_DTO;
using Patient_DTO;
using System;
using System.IO;
using System.Text.Json;

namespace Logger
{
    public class Logging
    {
        // Constructor
        public Logging()
        {

        }

        // Methods
        public void LogPatient(string? prefix, PatientDTO patientDTO)
        {
            var temp = $"{prefix}: {JsonSerializer.Serialize<PatientDTO>(patientDTO)}";
            StreamWriter str = new StreamWriter("Patient.txt", append: true);
            str.WriteLine(temp);
            str.Close();
        }
        public void LogDoctor(string prefix, DoctorDTO doctorDTO)
        {
            var temp = $"{prefix}: {JsonSerializer.Serialize<DoctorDTO>(doctorDTO)}";
            StreamWriter str = new StreamWriter("Doctor.txt", append: true);
            str.WriteLine(temp);
            str.Close();
        }
        public void LogAppointment(string prefix, AppointmentDTO appointmentDTO)
        {
            var temp = $"{prefix}: {JsonSerializer.Serialize<AppointmentDTO>(appointmentDTO)}";
            StreamWriter str = new StreamWriter("Appointment.txt", append: true);
            str.WriteLine(temp);
            str.Close();
        }
    }
}
