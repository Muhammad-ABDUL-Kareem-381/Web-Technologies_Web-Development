CREATE TABLE PatientPreferences (
        PreferenceId INT IDENTITY(1,1) PRIMARY KEY,
        PatientId INT NOT NULL,
        PreferredSpecialization NVARCHAR(200),
        PreferredDayOfWeek INT CHECK (PreferredDayOfWeek >= 0 AND PreferredDayOfWeek <= 6),
        PreferredTimeOfDay NVARCHAR(20),
        MaxDistance INT,
        MaxWaitingTime INT,
        PreferredLanguage NVARCHAR(50),
        UpdatedAt DATETIME2 DEFAULT GETDATE(),
        FOREIGN KEY (PatientId) REFERENCES Patients(PatientId) ON DELETE CASCADE
);