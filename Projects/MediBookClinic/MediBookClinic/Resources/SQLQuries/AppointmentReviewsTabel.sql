CREATE TABLE AppointmentReviews (
        ReviewId INT IDENTITY(1,1) PRIMARY KEY,
        AppointmentId INT NOT NULL,
        PatientId INT NOT NULL,
        DoctorId INT NOT NULL,
        Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 5),
        Review NVARCHAR(MAX),
        IsAnonymous BIT DEFAULT 0,
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        FOREIGN KEY (AppointmentId) REFERENCES Appointments(AppointmentId),
        FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
        FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId)
);