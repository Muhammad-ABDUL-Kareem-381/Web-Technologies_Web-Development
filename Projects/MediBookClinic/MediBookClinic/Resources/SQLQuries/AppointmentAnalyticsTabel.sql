CREATE TABLE AppointmentAnalytics (
    AnalyticsId INT IDENTITY(1,1) PRIMARY KEY,
    RecordDate DATE NOT NULL,
    TotalAppointments INT DEFAULT 0,
    CompletedAppointments INT DEFAULT 0,
    CancelledAppointments INT DEFAULT 0,
    NoShowAppointments INT DEFAULT 0,
    AverageWaitingTime DECIMAL(10,2), -- in days
    AverageRating DECIMAL(3,2),
    Revenue DECIMAL(12,2),
    CreatedAt DATETIME2 DEFAULT GETDATE()
);