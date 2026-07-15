using MediBookClinic.Models.Entities;

namespace MediBookClinic.Models.Interfaces.Service
{
    public interface IAvailabilityService
    {
        // Doctor Availability Methods
        Task<DoctorAvailability> CreateAvailabilityAsync(DoctorAvailability availability);
        Task<IEnumerable<DoctorAvailability>> GetDoctorAvailabilitiesAsync(int doctorId);
        Task<DoctorAvailability?> GetAvailabilityByDayAsync(int doctorId, string dayOfWeek);
        Task<bool> UpdateAvailabilityAsync(DoctorAvailability availability);
        Task<bool> DeleteAvailabilityAsync(int availabilityId);
        Task<bool> DeleteAllAvailabilitiesAsync(int doctorId);
        // Special Date Methods (Holidays, Leaves, Custom Hours)
        Task<DoctorSpecialDate> CreateSpecialDateAsync(DoctorSpecialDate specialDate);
        Task<IEnumerable<DoctorSpecialDate>> GetDoctorSpecialDatesAsync(int doctorId);
        Task<DoctorSpecialDate?> GetSpecialDateByDateAsync(int doctorId, DateTime date);
        Task<bool> UpdateSpecialDateAsync(DoctorSpecialDate specialDate);
        Task<bool> DeleteSpecialDateAsync(int specialDateId);
        Task<bool> IsDoctorAvailableOnDateAsync(int doctorId, DateTime date);
    }
}