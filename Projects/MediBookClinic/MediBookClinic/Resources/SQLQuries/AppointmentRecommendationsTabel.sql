CREATE TABLE AppointmentRecommendations (
        RecommendationId INT IDENTITY(1,1) PRIMARY KEY,
        PatientId INT NOT NULL,
        DoctorId INT NOT NULL,
        MatchScore DECIMAL(5,2) NOT NULL,
        ReasonForRecommendation NVARCHAR(500),
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
        FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId)
);