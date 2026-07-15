using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Repository
{
    public interface IAvailabilityRepository
    {
        Task<DoctorAvailability> CreateAsync(DoctorAvailability availability);
        Task<IEnumerable<DoctorAvailability>> GetByDoctorIdAsync(int doctorId);
        Task<DoctorAvailability?> GetByDayAsync(int doctorId, string dayOfWeek);
        Task<bool> UpdateAsync(DoctorAvailability availability);
        Task<bool> DeleteAsync(int availabilityId);
        Task<bool> DeleteAllByDoctorIdAsync(int doctorId);
    }
}