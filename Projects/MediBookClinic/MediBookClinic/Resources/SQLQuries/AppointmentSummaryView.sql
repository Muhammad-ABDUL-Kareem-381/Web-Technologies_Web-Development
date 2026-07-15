CREATE VIEW vw_AppointmentSummary AS
SELECT 
    a.AppointmentId,
    a.AppointmentDate,
    a.AppointmentTime,
    a.Status,
    CONCAT(p_user.FirstName, ' ', p_user.LastName) AS PatientName,
    p_user.PhoneNumber AS PatientPhone,
    CONCAT('Dr. ', d_user.FirstName, ' ', d_user.LastName) AS DoctorName,
    d.Specialization,
    d.ConsultationFee,
    a.ReasonForVisit,
    a.CreatedAt
FROM Appointments a
INNER JOIN Patients p ON a.PatientId = p.PatientId
INNER JOIN AspNetUsers p_user ON p.UserId = p_user.Id
INNER JOIN Doctors d ON a.DoctorId = d.DoctorId
INNER JOIN AspNetUsers d_user ON d.UserId = d_user.Id;