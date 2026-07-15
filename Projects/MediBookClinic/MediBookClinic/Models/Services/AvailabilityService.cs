using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;
using MediBookClinic.Models.Interfaces.Service;

namespace MediBookClinic.Models.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly ISpecialDateRepository _specialDateRepository;
        private readonly ILogger<AvailabilityService> _logger;

        public AvailabilityService(IAvailabilityRepository availabilityRepository,ISpecialDateRepository specialDateRepository,ILogger<AvailabilityService> logger)
        {
            _availabilityRepository = availabilityRepository;
            _specialDateRepository = specialDateRepository;
            _logger = logger;
        }

        public async Task<DoctorAvailability> CreateAvailabilityAsync(DoctorAvailability availability)
        {
            availability.CreatedAt = DateTime.UtcNow;
            availability.IsActive = true;
            return await _availabilityRepository.CreateAsync(availability);
        }

        public async Task<IEnumerable<DoctorAvailability>> GetDoctorAvailabilitiesAsync(int doctorId)
        {
            return await _availabilityRepository.GetByDoctorIdAsync(doctorId);
        }

        public async Task<DoctorAvailability?> GetAvailabilityByDayAsync(int doctorId, string dayOfWeek)
        {
            return await _availabilityRepository.GetByDayAsync(doctorId, dayOfWeek);
        }

        public async Task<bool> UpdateAvailabilityAsync(DoctorAvailability availability)
        {
            return await _availabilityRepository.UpdateAsync(availability);
        }

        public async Task<bool> DeleteAvailabilityAsync(int availabilityId)
        {
            return await _availabilityRepository.DeleteAsync(availabilityId);
        }

        public async Task<bool> DeleteAllAvailabilitiesAsync(int doctorId)
        {
            return await _availabilityRepository.DeleteAllByDoctorIdAsync(doctorId);
        }

        public async Task<DoctorSpecialDate> CreateSpecialDateAsync(DoctorSpecialDate specialDate)
        {
            specialDate.CreatedAt = DateTime.UtcNow;
            return await _specialDateRepository.CreateAsync(specialDate);
        }

        public async Task<IEnumerable<DoctorSpecialDate>> GetDoctorSpecialDatesAsync(int doctorId)
        {
            return await _specialDateRepository.GetByDoctorIdAsync(doctorId);
        }

        public async Task<DoctorSpecialDate?> GetSpecialDateByDateAsync(int doctorId, DateTime date)
        {
            return await _specialDateRepository.GetByDateAsync(doctorId, date);
        }

        public async Task<bool> UpdateSpecialDateAsync(DoctorSpecialDate specialDate)
        {
            return await _specialDateRepository.UpdateAsync(specialDate);
        }

        public async Task<bool> DeleteSpecialDateAsync(int specialDateId)
        {
            return await _specialDateRepository.DeleteAsync(specialDateId);
        }

        public async Task<bool> IsDoctorAvailableOnDateAsync(int doctorId, DateTime date)
        {
            // First check special dates
            var specialDate = await _specialDateRepository.GetByDateAsync(doctorId, date);
            if (specialDate != null)
            {
                return specialDate.IsAvailable;
            }

            // Then check regular availability
            var dayOfWeek = ((int)date.DayOfWeek).ToString();
            var availability = await _availabilityRepository.GetByDayAsync(doctorId, dayOfWeek);
            return availability?.IsActive ?? false;
        }
    }
}