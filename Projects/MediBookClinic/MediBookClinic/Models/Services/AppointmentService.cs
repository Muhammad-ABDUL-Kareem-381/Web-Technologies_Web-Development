using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;
using MediBookClinic.Models.Interfaces.Service;

namespace MediBookClinic.Models.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(IAppointmentRepository appointmentRepository,IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,IAvailabilityRepository availabilityRepository,ILogger<AppointmentService> logger)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _availabilityRepository = availabilityRepository;
            _logger = logger;
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            try
            {
                // Validate slot availability
                var isAvailable = await _appointmentRepository.IsSlotAvailableAsync(
                    appointment.DoctorId,
                    appointment.AppointmentDate,
                    appointment.AppointmentTime);

                if (!isAvailable)
                {
                    throw new InvalidOperationException("The selected time slot is not available.");
                }

                appointment.Status = "Pending";
                appointment.CreatedAt = DateTime.UtcNow;
                appointment.UpdatedAt = DateTime.UtcNow;

                var createdAppointment = await _appointmentRepository.CreateAsync(appointment);
                _logger.LogInformation("Appointment created successfully with ID: {AppointmentId}", createdAppointment.AppointmentId);

                return createdAppointment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating appointment");
                throw;
            }
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _appointmentRepository.GetByIdAsync(appointmentId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _appointmentRepository.GetByDoctorIdAsync(doctorId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return await _appointmentRepository.GetByPatientIdAsync(patientId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateRangeAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            return await _appointmentRepository.GetByDateRangeAsync(doctorId, startDate, endDate);
        }

        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            appointment.UpdatedAt = DateTime.UtcNow;
            return await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task<bool> UpdateAppointmentStatusAsync(int appointmentId, string status)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return false;
            }
            appointment.Status = status;
            appointment.UpdatedAt = DateTime.UtcNow;

            if (status == "Completed")
            {
                appointment.CompletedAt = DateTime.UtcNow;
            }
            else if (status == "Cancelled")
            {
                appointment.CancelledAt = DateTime.UtcNow;
            }

            return await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task<bool> CancelAppointmentAsync(int appointmentId, string cancelledBy, string? cancellationReason = null)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return false;
            }
            appointment.Status = "Cancelled";
            appointment.CancellationReason = cancellationReason;
            appointment.CancelledAt = DateTime.UtcNow;
            appointment.UpdatedAt = DateTime.UtcNow;

            return await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task<bool> DeleteAppointmentAsync(int appointmentId)
        {
            return await _appointmentRepository.DeleteAsync(appointmentId);
        }

        public async Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync(int doctorId, int days = 7)
        {
            return await _appointmentRepository.GetUpcomingAsync(doctorId, days);
        }

        public async Task<IEnumerable<Appointment>> GetPastAppointmentsAsync(int patientId)
        {
            return await _appointmentRepository.GetPastAsync(patientId);
        }

        public async Task<bool> IsSlotAvailableAsync(int doctorId, DateTime appointmentDate, TimeSpan appointmentTime)
        {
            return await _appointmentRepository.IsSlotAvailableAsync(doctorId, appointmentDate, appointmentTime);
        }

        public async Task<IEnumerable<TimeSpan>> GetAvailableSlotsAsync(int doctorId, DateTime date)
        {
            // ✅ FIXED: Convert DateTime.DayOfWeek to string for repository
            var dayOfWeek = ((int)date.DayOfWeek).ToString();
            var availability = await _availabilityRepository.GetByDayAsync(doctorId, dayOfWeek);

            if (availability == null || !availability.IsActive)
            {
                return new List<TimeSpan>();
            }
            var slots = new List<TimeSpan>();
            var currentTime = availability.StartTime;

            while (currentTime < availability.EndTime)
            {
                var isAvailable = await _appointmentRepository.IsSlotAvailableAsync(doctorId, date, currentTime);
                if (isAvailable)
                {
                    slots.Add(currentTime);
                }
                currentTime = currentTime.Add(TimeSpan.FromMinutes(availability.SlotDuration));
            }

            return slots;
        }

        public async Task<int> GetTodayAppointmentCountAsync(int doctorId)
        {
            return await _appointmentRepository.GetTodayCountAsync(doctorId);
        }
    }
}