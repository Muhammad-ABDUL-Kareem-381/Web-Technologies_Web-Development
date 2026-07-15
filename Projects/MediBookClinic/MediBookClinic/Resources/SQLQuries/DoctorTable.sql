CREATE TABLE Doctors (
        DoctorId INT IDENTITY(1,1) PRIMARY KEY,
        UserId NVARCHAR(450) NOT NULL UNIQUE,
        Specialization NVARCHAR(200) NOT NULL,
        LicenseNumber NVARCHAR(100) NOT NULL UNIQUE,
        YearsOfExperience INT NOT NULL CHECK (YearsOfExperience >= 0),
        Qualification NVARCHAR(500) NOT NULL,
        Biography NVARCHAR(MAX),
        ConsultationFee DECIMAL(10,2) NOT NULL CHECK (ConsultationFee >= 0),
        Rating DECIMAL(3,2) DEFAULT 0.00 CHECK (Rating >= 0 AND Rating <= 5),
        TotalReviews INT DEFAULT 0,
        IsAvailableForBooking BIT DEFAULT 1,
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        UpdatedAt DATETIME2 DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);