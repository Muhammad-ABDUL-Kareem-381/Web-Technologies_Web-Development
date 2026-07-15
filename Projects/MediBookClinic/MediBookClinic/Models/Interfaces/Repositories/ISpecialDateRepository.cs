using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Repository
{
    public interface ISpecialDateRepository
    {
        Task<DoctorSpecialDate> CreateAsync(DoctorSpecialDate specialDate);
        Task<IEnumerable<DoctorSpecialDate>> GetByDoctorIdAsync(int doctorId);
        Task<DoctorSpecialDate?> GetByDateAsync(int doctorId, DateTime date);
        Task<bool> UpdateAsync(DoctorSpecialDate specialDate);
        Task<bool> DeleteAsync(int specialDateId);
    }
}