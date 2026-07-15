using MediBookClinic.Data;
using MediBookClinic.Models.Adapters;
using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;
using MediBookClinic.Models.Interfaces.Service;
using MediBookClinic.Models.Repositories;
using MediBookClinic.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MediBookClinic.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using MediBookClinic.Authorization.Requirments;
using MediBookClinic.Authorization.Operations;
using MediBookClinic.Authorization.Handlers;

namespace MediBookClinic
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Register Dapper Context
            builder.Services.AddScoped<DapperContext>();

            // Configure Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password Settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout Settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User Settings
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                // Sign In Settings
                options.SignIn.RequireConfirmedEmail = false; 
                options.SignIn.RequireConfirmedPhoneNumber = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders()
              .AddDefaultUI();

            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Keep PascalCase
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            builder.Services.AddRazorPages();

            // Cookie Configuration
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.Cookie.Name = "MediBookClinic.Auth";
            });

            // Configure Srevices
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

            // Configure Repositories
            builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IPatientPreferenceRepository, PatientPreferenceRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<ISpecialDateRepository, SpecialDateRepository>();

            // Configure Email Settings and Service
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddTransient<IEmailService, EmailService>();

            // Configure SMS Settings and Service
            builder.Services.Configure<SmsSettings>(builder.Configuration.GetSection("SmsSettings"));
            builder.Services.AddTransient<ISmsService, SmsService>();

            // Configure Authorization Handlers (Resource-Based)
            builder.Services.AddScoped<IAuthorizationHandler, AppointmentOperationAuthorizationHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, DoctorProfileOperationAuthorizationHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, PatientRecordOperationAuthorizationHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, ReviewOperationAuthorizationHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, EmailVerifiedHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, ActiveAccountHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, DoctorAvailableHandler>();

            // Configure Authorization (All Types)
            builder.Services.AddAuthorization(options =>
            {
                // ROLE-BASED POLICIES

            //    options.AddPolicy("MasterAdminOnly", policy =>
            //        policy.RequireRole("MasterAdmin"));

            //    options.AddPolicy("AdminOnly", policy =>
            //        policy.RequireRole("Admin"));

            //    options.AddPolicy("DoctorOnly", policy =>
            //        policy.RequireRole("Doctor"));

            //    options.AddPolicy("PatientOnly", policy =>
            //        policy.RequireRole("Patient"));

            //    options.AddPolicy("AdminOrMasterAdmin", policy =>
            //        policy.RequireRole("Admin", "MasterAdmin"));

            //    options.AddPolicy("DoctorOrAdmin", policy =>
            //        policy.RequireRole("Doctor", "Admin", "MasterAdmin"));

            //    options.AddPolicy("PatientOrAdmin", policy =>
            //        policy.RequireRole("Patient", "Admin", "MasterAdmin"));

            //    options.AddPolicy("DoctorOrPatient", policy =>
            //        policy.RequireRole("Doctor", "Patient"));

            //    // CLAIM-BASED POLICIES

            //    // User Type Claims
            //    options.AddPolicy("IsMasterAdmin", policy =>
            //        policy.RequireClaim("UserType", "MasterAdmin"));

            //    options.AddPolicy("IsAdminUser", policy =>
            //        policy.RequireClaim("UserType", "Admin"));

            //    options.AddPolicy("IsDoctorUser", policy =>
            //        policy.RequireClaim("UserType", "Doctor"));

            //    options.AddPolicy("IsPatientUser", policy =>
            //        policy.RequireClaim("UserType", "Patient"));

            //    // Entity ID Claims (for accessing own records)
            //    options.AddPolicy("HasDoctorId", policy =>
            //        policy.RequireClaim("DoctorId"));

            //    options.AddPolicy("HasPatientId", policy =>
            //        policy.RequireClaim("PatientId"));

            //    // Permission Claims
            //    options.AddPolicy("CanManageAdmins", policy =>
            //        policy.RequireClaim("Permission", "ManageAdmins")); // Only MasterAdmin

            //    options.AddPolicy("CanApproveUsers", policy =>
            //        policy.RequireRole("Admin", "MasterAdmin"));

            //    options.AddPolicy("CanManageAppointments", policy =>
            //        policy.RequireClaim("Permission", "ManageAppointments"));

            //    options.AddPolicy("CanViewAllAppointments", policy =>
            //        policy.RequireClaim("Permission", "ViewAllAppointments"));

            //    options.AddPolicy("CanManageUsers", policy =>
            //        policy.RequireClaim("Permission", "ManageUsers"));

            //    options.AddPolicy("CanViewReports", policy =>
            //        policy.RequireClaim("Permission", "ViewReports"));

            //    options.AddPolicy("CanManageSystemSettings", policy =>
            //        policy.RequireClaim("Permission", "ManageSystemSettings"));

            //    // COMBINED ROLE + CLAIM POLICIES

            //    options.AddPolicy("DoctorWithProfile", policy =>
            //        policy.RequireRole("Doctor")
            //              .RequireClaim("DoctorId"));

            //    options.AddPolicy("PatientWithProfile", policy =>
            //        policy.RequireRole("Patient")
            //              .RequireClaim("PatientId"));

            //    options.AddPolicy("ActiveDoctor", policy =>
            //        policy.RequireRole("Doctor")
            //              .RequireClaim("DoctorId")
            //              .RequireClaim("IsActive", "True"));

            //    options.AddPolicy("ActivePatient", policy =>
            //        policy.RequireRole("Patient")
            //              .RequireClaim("PatientId")
            //              .RequireClaim("IsActive", "True"));

            //    // CUSTOM REQUIREMENT POLICIES

            //    // Minimum Age Requirement
            //    options.AddPolicy("MinimumAge18", policy =>
            //        policy.Requirements.Add(new MinimumAgeRequirement(18)));

            //    // Email Verified Requirement
            //    options.AddPolicy("EmailVerified", policy =>
            //        policy.Requirements.Add(new EmailVerifiedRequirement()));

            //    // Account Active Requirement
            //    options.AddPolicy("ActiveAccount", policy =>
            //        policy.Requirements.Add(new ActiveAccountRequirement()));

            //    // Doctor Available for Booking
            //    options.AddPolicy("DoctorAvailableForBooking", policy =>
            //        policy.RequireRole("Doctor")
            //              .Requirements.Add(new DoctorAvailableRequirement()));

            //    // RESOURCE-BASED POLICIES (Used with IAuthorizationService)

            //    // Appointment Operations
            //    options.AddPolicy("ViewAppointment", policy =>
            //        policy.Requirements.Add(new AppointmentOperationAuthorizationRequirement(AppointmentOperations.Read)));

            //    options.AddPolicy("CreateAppointment", policy =>
            //        policy.Requirements.Add(new AppointmentOperationAuthorizationRequirement(AppointmentOperations.Create)));

            //    options.AddPolicy("UpdateAppointment", policy =>
            //        policy.Requirements.Add(new AppointmentOperationAuthorizationRequirement(AppointmentOperations.Update)));

            //    options.AddPolicy("DeleteAppointment", policy =>
            //        policy.Requirements.Add(new AppointmentOperationAuthorizationRequirement(AppointmentOperations.Delete)));

            //    options.AddPolicy("CancelAppointment", policy =>
            //        policy.Requirements.Add(new AppointmentOperationAuthorizationRequirement(AppointmentOperations.Cancel)));

            //    // Doctor Profile Operations
            //    options.AddPolicy("ViewDoctorProfile", policy =>
            //        policy.Requirements.Add(new DoctorProfileOperationAuthorizationRequirement(DoctorProfileOperations.Read)));

            //    options.AddPolicy("UpdateDoctorProfile", policy =>
            //        policy.Requirements.Add(new DoctorProfileOperationAuthorizationRequirement(DoctorProfileOperations.Update)));

            //    options.AddPolicy("ManageDoctorAvailability", policy =>
            //        policy.Requirements.Add(new DoctorProfileOperationAuthorizationRequirement(DoctorProfileOperations.ManageAvailability)));

            //    // Patient Record Operations
            //    options.AddPolicy("ViewPatientRecord", policy =>
            //        policy.Requirements.Add(new PatientRecordOperationAuthorizationRequirement(PatientRecordOperations.Read)));

            //    options.AddPolicy("UpdatePatientRecord", policy =>
            //        policy.Requirements.Add(new PatientRecordOperationAuthorizationRequirement(PatientRecordOperations.Update)));

            //    options.AddPolicy("ViewMedicalHistory", policy =>
            //        policy.Requirements.Add(new PatientRecordOperationAuthorizationRequirement(PatientRecordOperations.ViewMedicalHistory)));

            //    // Review Operations
            //    options.AddPolicy("CreateReview", policy =>
            //        policy.Requirements.Add(new ReviewOperationAuthorizationRequirement(ReviewOperations.Create)));

            //    options.AddPolicy("UpdateReview", policy =>
            //        policy.Requirements.Add(new ReviewOperationAuthorizationRequirement(ReviewOperations.Update)));

            //    options.AddPolicy("DeleteReview", policy =>
            //        policy.Requirements.Add(new ReviewOperationAuthorizationRequirement(ReviewOperations.Delete)));

            //    // COMPOSITE POLICIES (Multiple Requirements)

            //    options.AddPolicy("BookAppointmentPolicy", policy =>
            //    {
            //        policy.RequireRole("Patient");
            //        policy.RequireClaim("PatientId");
            //        policy.Requirements.Add(new ActiveAccountRequirement());
            //        policy.Requirements.Add(new EmailVerifiedRequirement());
            //    });

            //    options.AddPolicy("ManageAvailabilityPolicy", policy =>
            //    {
            //        policy.RequireRole("Doctor");
            //        policy.RequireClaim("DoctorId");
            //        policy.Requirements.Add(new ActiveAccountRequirement());
            //    });

            //    options.AddPolicy("AdminManagementPolicy", policy =>
            //    {
            //        policy.RequireRole("Admin");
            //        policy.RequireClaim("Permission", "ManageUsers", "ManageSystemSettings");
            //        policy.Requirements.Add(new ActiveAccountRequirement());
            //    });
            //});

            //// Configure Session
            //builder.Services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //    options.Cookie.Name = "MediBookClinic.Session";
            });

            // Configure SignalR
            builder.Services.AddSignalR();

            // Replace default IEmailSender with our custom EmailService
            builder.Services.AddTransient<IEmailSender>(provider =>
            {
                var emailService = provider.GetRequiredService<IEmailService>();
                return new EmailSenderAdapter(emailService);
            });

            // Configure Looging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.AddEventSourceLogger();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Use Routing
            app.UseRouting();

            // Use Authorization, and Authentication
            app.UseAuthentication();
            app.UseAuthorization();

            // Use Session
            app.UseSession();

            // Seeding Roles
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await DbSeeder.SeedRolesAsync(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding roles.");
                }
            }
            // Create Master Admin User
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await DbSeeder.CreateMasterAdminAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while creating the master admin user.");
                }
            }

                app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
