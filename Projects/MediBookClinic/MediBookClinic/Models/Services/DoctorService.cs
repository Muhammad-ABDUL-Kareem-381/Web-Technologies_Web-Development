using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;
using MediBookClinic.Models.Interfaces.Service;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;

namespace MediBookClinic.Models.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly ISpecialDateRepository _specialDateRepository;
        private readonly ILogger<DoctorService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public DoctorService(IDoctorRepository doctorRepository,IAppointmentRepository appointmentRepository,IReviewRepository reviewRepository,
            IAvailabilityRepository availabilityRepository,ISpecialDateRepository specialDateRepository,ILogger<DoctorService> logger,
            UserManager<ApplicationUser> userManager,IEmailService emailService)
        {
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _reviewRepository = reviewRepository;
            _availabilityRepository = availabilityRepository;
            _specialDateRepository = specialDateRepository;
            _logger = logger;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
        {
            try
            {
                doctor.IsAvailableForBooking = true;
                doctor.Rating = 0.00m;
                doctor.TotalReviews = 0;
                doctor.CreatedAt = DateTime.UtcNow;
                doctor.UpdatedAt = DateTime.UtcNow;

                var createdDoctor = await _doctorRepository.CreateAsync(doctor);
                _logger.LogInformation("Doctor created successfully with ID: {DoctorId}", createdDoctor.DoctorId);
                return createdDoctor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating doctor for UserId: {UserId}", doctor.UserId);
                throw;
            }
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
        {
            return await _doctorRepository.GetByIdAsync(doctorId);
        }

        public async Task<Doctor?> GetDoctorByUserIdAsync(string userId)
        {
            return await _doctorRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecializationAsync(string specialization)
        {
            return await _doctorRepository.GetBySpecializationAsync(specialization);
        }

        public async Task<IEnumerable<Doctor>> SearchDoctorsAsync(string searchTerm, string? specialization = null, string? city = null)
        {
            return await _doctorRepository.SearchAsync(searchTerm, specialization, city);
        }

        public async Task<bool> UpdateDoctorAsync(Doctor doctor)
        {
            try
            {
                doctor.UpdatedAt = DateTime.UtcNow;
                return await _doctorRepository.UpdateAsync(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating doctor with ID: {DoctorId}", doctor.DoctorId);
                throw;
            }
        }

        public async Task<bool> UpdateDoctorStatusAsync(int doctorId, string status)
        {
            var doctor = await _doctorRepository.GetByIdAsync(doctorId);
            if (doctor == null)
            {
                return false;
            }
            doctor.IsAvailableForBooking = status.ToLower() == "active";
            doctor.UpdatedAt = DateTime.UtcNow;
            return await _doctorRepository.UpdateAsync(doctor);
        }

        public async Task<bool> DeleteDoctorAsync(int doctorId)
        {
            try
            {
                return await _doctorRepository.DeleteAsync(doctorId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting doctor with ID: {DoctorId}", doctorId);
                throw;
            }
        }

        public async Task<IEnumerable<Doctor>> GetTopRatedDoctorsAsync(int count = 10)
        {
            return await _doctorRepository.GetTopRatedAsync(count);
        }

        public async Task<decimal> GetDoctorAverageRatingAsync(int doctorId)
        {
            return await _reviewRepository.GetAverageRatingAsync(doctorId);
        }

        public async Task<int> GetDoctorTotalAppointmentsAsync(int doctorId)
        {
            var appointments = await _appointmentRepository.GetByDoctorIdAsync(doctorId);
            return appointments.Count();
        }

        public async Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime date, TimeSpan time)
        {
            var dayOfWeek = ((int)date.DayOfWeek).ToString();
            var availability = await _availabilityRepository.GetByDayAsync(doctorId, dayOfWeek);

            if (availability == null || !availability.IsActive)
            {
                return false;
            }
            // Check if time is within available hours
            if (time < availability.StartTime || time >= availability.EndTime)
            {
                return false;
            }
            // Check special dates (holidays, leaves)
            var specialDate = await _specialDateRepository.GetByDateAsync(doctorId, date);
            if (specialDate != null && !specialDate.IsAvailable)
            {
                return false;
            }
            // Check if slot is already booked
            return await _appointmentRepository.IsSlotAvailableAsync(doctorId, date, time);
        }
        public async Task<IEnumerable<Doctor>> GetPendingDoctorsAsync()
        {
            try
            {
                var allDoctors = await _doctorRepository.GetAllAsync();

                // Filter doctors where IsAvailableForBooking = false (pending)
                return allDoctors.Where(d => !d.IsAvailableForBooking).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving pending doctors");
                throw;
            }
        }

        public async Task<bool> ApproveDoctorAsync(int doctorId, string approvedByUserId)
        {
            try
            {
                var doctor = await _doctorRepository.GetByIdAsync(doctorId);
                if (doctor == null)
                {
                    _logger.LogWarning("Doctor with ID {DoctorId} not found", doctorId);
                    return false;
                }

                // Get the ApplicationUser for this doctor
                var user = await _userManager.FindByIdAsync(doctor.UserId);
                if (user == null)
                {
                    _logger.LogWarning("User not found for DoctorId {DoctorId}", doctorId);
                    return false;
                }

                // Update ApplicationUser.IsActive
                user.IsActive = true;
                user.UpdatedAt = DateTime.UtcNow;
                var userResult = await _userManager.UpdateAsync(user);

                if (!userResult.Succeeded)
                {
                    _logger.LogError("Failed to update user active status for DoctorId {DoctorId}", doctorId);
                    return false;
                }

                // Update IsActive claim
                var existingClaims = await _userManager.GetClaimsAsync(user);
                var isActiveClaim = existingClaims.FirstOrDefault(c => c.Type == "IsActive");

                if (isActiveClaim != null)
                {
                    await _userManager.RemoveClaimAsync(user, isActiveClaim);
                }
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsActive", "True"));

                // Add permissions for approved doctor
                await _userManager.AddClaimsAsync(user, new[]
                {
                    new System.Security.Claims.Claim("Permission", "ManageAppointments"),
                    new System.Security.Claims.Claim("Permission", "ManageAvailability"),
                    new System.Security.Claims.Claim("Permission", "ViewPatientRecords"),
                    new System.Security.Claims.Claim("Permission", "UpdateMedicalHistory"),
                    new System.Security.Claims.Claim("Permission", "ViewOwnAppointments"),
                    new System.Security.Claims.Claim("Permission", "ManageProfile")
                });

                // Update IsAvailableForBooking claim
                var availableClaim = existingClaims.FirstOrDefault(c => c.Type == "IsAvailableForBooking");
                if (availableClaim != null)
                {
                    await _userManager.RemoveClaimAsync(user, availableClaim);
                }
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsAvailableForBooking", "True"));

                // Update database
                doctor.IsAvailableForBooking = true;
                await _doctorRepository.UpdateAsync(doctor);

                // Send approval email
                await _emailService.SendEmailAsync(
                    user.Email,
                    "Account Approved - Welcome to MediBook Clinic",
                    $@"<html>
                        <body style='font-family: Arial, sans-serif;'>
                            <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                                <h2 style='color: #4CAF50;'>Account Approved!</h2>
                                <p>Dear Dr. {user.FirstName} {user.LastName},</p>
                                <p><strong>Great news! Your account has been approved.</strong></p>
                                <p>You can now log in and access all features:</p>
                                <ul>
                                    <li>Manage your availability</li>
                                    <li>View and manage appointments</li>
                                    <li>Update your profile</li>
                                    <li>View patient reviews</li>
                                </ul>
                                <p>Thank you for joining MediBook Clinic!</p>
                                <p><a href='{HtmlEncoder.Default.Encode("https://localhost:7000/Identity/Account/Login")}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; display: inline-block; margin-top: 10px;'>Login Now</a></p>
                            </div>
                        </body>
                       </html>"
                );

                _logger.LogInformation("Doctor {DoctorId} approved by user {ApprovedBy}", doctorId, approvedByUserId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving doctor {DoctorId}", doctorId);
                throw;
            }
        }

        public async Task<bool> RejectDoctorAsync(int doctorId, string reason, string rejectedByUserId)
        {
            try
            {
                var doctor = await _doctorRepository.GetByIdAsync(doctorId);
                if (doctor == null)
                {
                    _logger.LogWarning("Doctor with ID {DoctorId} not found", doctorId);
                    return false;
                }

                // Get the ApplicationUser
                var user = await _userManager.FindByIdAsync(doctor.UserId);
                if (user == null)
                {
                    _logger.LogWarning("User not found for DoctorId {DoctorId}", doctorId);
                    return false;
                }

                // Send rejection email
                await _emailService.SendEmailAsync(
                    user.Email,
                    "Registration Application Status",
                    $@"<html>
                        <body style='font-family: Arial, sans-serif;'>
                            <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                                <h2 style='color: #f44336;'>Registration Update</h2>
                                <p>Dear Dr. {user.FirstName} {user.LastName},</p>
                                <p>Thank you for your interest in joining MediBook Clinic.</p>
                                <p>After reviewing your application, we are unable to approve your registration at this time.</p>
                                <p><strong>Reason:</strong> {reason}</p>
                                <p>If you believe this is an error or would like to discuss this further, please contact our support team.</p>
                                <p style='margin-top: 30px; color: #666; font-size: 12px;'>
                                    Support Email: support@medibookclinic.com
                                </p>
                            </div>
                        </body>
                       </html>"
                );

                // Delete doctor record
                await _doctorRepository.DeleteAsync(doctorId);

                // Delete user account
                await _userManager.DeleteAsync(user);

                _logger.LogInformation("Doctor {DoctorId} rejected by user {RejectedBy}. Reason: {Reason}", doctorId, rejectedByUserId, reason);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rejecting doctor {DoctorId}", doctorId);
                throw;
            }
        }
    }
}