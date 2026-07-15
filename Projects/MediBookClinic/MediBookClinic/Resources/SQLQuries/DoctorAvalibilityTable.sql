CREATE TABLE DoctorAvailability (
        AvailabilityId INT IDENTITY(1,1) PRIMARY KEY,
        DoctorId INT NOT NULL,
        DayOfWeek INT NOT NULL CHECK (DayOfWeek >= 0 AND DayOfWeek <= 6),
        StartTime TIME NOT NULL,
        EndTime TIME NOT NULL,
        SlotDuration INT NOT NULL DEFAULT 30 CHECK (SlotDuration > 0),
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId) ON DELETE CASCADE,
        CONSTRAINT CK_TimeRange CHECK (EndTime > StartTime)
);