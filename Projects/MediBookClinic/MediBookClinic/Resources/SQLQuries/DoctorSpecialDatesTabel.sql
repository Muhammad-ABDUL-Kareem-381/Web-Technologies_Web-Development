CREATE TABLE DoctorSpecialDates (
        SpecialDateId INT IDENTITY(1,1) PRIMARY KEY,
        DoctorId INT NOT NULL,
        SpecialDate DATE NOT NULL,
        IsAvailable BIT NOT NULL,
        StartTime TIME,
        EndTime TIME,
        Reason NVARCHAR(200),
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId) ON DELETE CASCADE
);