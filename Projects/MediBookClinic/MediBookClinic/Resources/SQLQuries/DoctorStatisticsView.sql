CREATE VIEW vw_DoctorStatistics AS
SELECT 
    d.DoctorId,
    CONCAT('Dr. ', u.FirstName, ' ', u.LastName) AS DoctorName,
    d.Specialization,
    d.Rating,
    d.TotalReviews,
    d.ConsultationFee,
    COUNT(a.AppointmentId) AS TotalAppointments,
    SUM(CASE WHEN a.Status = 'Completed' THEN 1 ELSE 0 END) AS CompletedAppointments,
    SUM(CASE WHEN a.Status = 'Completed' THEN d.ConsultationFee ELSE 0 END) AS TotalRevenue
FROM Doctors d
INNER JOIN AspNetUsers u ON d.UserId = u.Id
LEFT JOIN Appointments a ON d.DoctorId = a.DoctorId
GROUP BY d.DoctorId, u.FirstName, u.LastName, d.Specialization, d.Rating, d.TotalReviews, d.ConsultationFee;