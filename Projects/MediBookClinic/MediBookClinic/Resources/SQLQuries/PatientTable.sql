CREATE TABLE Patients (
        PatientId INT IDENTITY(1,1) PRIMARY KEY,
        UserId NVARCHAR(450) NOT NULL UNIQUE,
        BloodGroup NVARCHAR(10),
        Allergies NVARCHAR(500),
        MedicalHistory NVARCHAR(MAX),
        EmergencyContactName NVARCHAR(200),
        EmergencyContactPhone NVARCHAR(20),
        InsuranceProvider NVARCHAR(200),
        InsuranceNumber NVARCHAR(100),
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        UpdatedAt DATETIME2 DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);