INSERT INTO dbo.SystemSettings (SettingKey, SettingValue, Description, UpdatedAt)
VALUES
    ('DefaultAppointmentDuration', '30', 'Default appointment duration in minutes', GETDATE()),
    ('MaxAdvanceBookingDays', '90', 'Maximum days in advance for booking', GETDATE()),
    ('CancellationDeadlineHours', '24', 'Hours before appointment for cancellation', GETDATE()),
    ('EmailNotificationsEnabled', 'true', 'Enable email notifications', GETDATE()),
    ('SMSNotificationsEnabled', 'true', 'Enable SMS notifications', GETDATE()),
    ('DefaultTheme', 'light', 'Default application theme', GETDATE()),
    ('DefaultLanguage', 'en', 'Default system language', GETDATE()),
    ('ClinicName', 'HealthCare Clinic', 'Clinic name', GETDATE()),
    ('ClinicAddress', 'Lahore, Punjab, Pakistan', 'Clinic address', GETDATE()),
    ('ClinicPhone', '+92-300-1234567', 'Clinic contact number', GETDATE()),
    ('ClinicEmail', 'info@healthcareclinic.com', 'Clinic email', GETDATE());
    