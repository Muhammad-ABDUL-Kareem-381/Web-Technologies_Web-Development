using System;
using Patient_DAL;
using Patient_DTO;

namespace Patient_BLL
{
    public class PatientBLL
    {
        public bool Exists(string cnic)
        {
            if (cnic == null)
            {
                return false;
            }
            else
            {
                return new PatientDAL().Exists(cnic);
            }
        }
        public List<PatientDTO> GetAll()
        {
            return new PatientDAL().GetAll();
        }
        public void Insert(PatientDTO patientDTO)
        {
            new PatientDAL().Insert(patientDTO);
        }
        public void Update(PatientDTO patientDTO)
        {
            new PatientDAL().Update(patientDTO);
        }
        public void Delete(string cnic)
        {
            new PatientDAL().Delete(cnic);
        }
        public PatientDTO GetByCnic(string cnic)
        {
            return new PatientDAL().GetByCnic(cnic);
        }
        public bool HasAppointment(PatientDTO patientDTO, int appointmentID)
        {
            if (patientDTO == null)
            {
                return false;
            }
            else if (appointmentID <= 0)
            {
                return false;
            }
            else if (patientDTO.Appointments.Count == 0)
            {
                return false;
            }
            else
            {
                bool search = false;
                foreach (var appointment in patientDTO.Appointments)
                {
                    if (appointment == appointmentID)
                    {
                        search = true;
                        break;
                    }
                    else
                    {

                    }
                }
                return search;
            }
        }
        public void AddAppointment(PatientDTO patientDTO, int appointmentID)
        {
            if (patientDTO == null)
            {
                return;
            }
            else if (appointmentID <= 0)
            {
                return;
            }
            else if (patientDTO.Appointments.Count == 0)
            {
                patientDTO.Appointments.Add(appointmentID);
                return;
            }
            else if (!HasAppointment(patientDTO, appointmentID))
            {
                patientDTO.Appointments.Add(appointmentID);
                return;
            }
            else
            {
                return;
            }
        }
        public void RemoveAppointment(PatientDTO patientDTO, int appointmentID)
        {
            if (patientDTO == null)
            {
                return;
            }
            else if (appointmentID <= 0)
            {
                return;
            }
            else if (patientDTO.Appointments.Count == 0)
            {
                return;
            }
            else if (HasAppointment(patientDTO, appointmentID))
            {
                patientDTO.Appointments.Remove(appointmentID);
                return;
            }
            else
            {
                return;
            }
        }
        public string ToString(PatientDTO patientDTO)
        {
            if (patientDTO == null)
            {
                return string.Empty;
            }
            else
            {
                string appointmentsList = "";
                for (int i = 0; i < patientDTO.Appointments.Count; i++)
                {
                    appointmentsList += patientDTO.Appointments[i];

                    if (i < patientDTO.Appointments.Count - 1)
                    {
                        appointmentsList += ",";
                    }
                }
                return $"Name: {patientDTO.Name}, CNIC: {patientDTO.CNIC}, Appointments: [{appointmentsList}]";
            }
        }
    }
}

