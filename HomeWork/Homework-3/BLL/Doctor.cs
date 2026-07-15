using Doctor_DAL;
using Doctor_DTO;
using Microsoft.Data.SqlClient;
using System;

namespace Doctor_BLL
{
    public class DoctorBLL
    {
        // Public Methods
        public int GenerateDoctorID() // Generate Doctor ID
        {
            var rand = new Random();
            return rand.Next(1, 1000000);
        }
        public bool Exists(int doctorId)
        {
            if (doctorId <= 0)
            {
                return false;
            }
            else
            {
                return new DoctorDAL().Exists(doctorId);
            }
        }
        public DoctorDTO GetById(int doctorId)
        {
            if (doctorId <= 0)
            {
                return new DoctorDTO();
            }
            else
            {
                return new DoctorDAL().GetById(doctorId);
            }
        }
        public List<DoctorDTO> GetAll()
        {
            return new DoctorDAL().GetAll();
        }
        public void Insert(DoctorDTO doctorDTO)
        {
            new DoctorDAL().Insert(doctorDTO);
        }
        public void Update(DoctorDTO doctorDTO)
        {
            new DoctorDAL().Update(doctorDTO);
        }
        public void Delete(int doctorId)
        {
            new DoctorDAL().Delete(doctorId);
        }
        public void MarkUnavailable(DoctorDTO doctorDTO)
        {
            if (doctorDTO == null)
            {
                return;
            }
            else if (doctorDTO.IsAvailable)
            {
                doctorDTO.IsAvailable = false;
            }
            else
            {
                return;
            }
        }
        public void MarkAvailable(DoctorDTO doctorDTO)
        {
            if (doctorDTO == null)
            {
                return;
            }
            else if (doctorDTO.IsAvailable)
            {
                return;
            }
            else
            {
                doctorDTO.IsAvailable = true; ;
            }
        }
        public string ToString(DoctorDTO doctorDTO)
        {
            if (doctorDTO == null)
            {
                return string.Empty;
            }
            else
            {
                return $"DoctorID: {doctorDTO.DoctorID}, Name: {doctorDTO.Name}, Specialization: {doctorDTO.Specialization}, Available: {doctorDTO.IsAvailable}";
            }
        }
    }
}

