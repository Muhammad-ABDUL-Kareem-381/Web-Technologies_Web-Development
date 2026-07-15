CREATE PROCEDURE sp_UpdateDoctorRating
    @DoctorId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Doctors
    SET Rating = (
        SELECT AVG(CAST(Rating AS DECIMAL(3,2)))
        FROM AppointmentReviews
        WHERE DoctorId = @DoctorId
    ),
    TotalReviews = (
        SELECT COUNT(*)
        FROM AppointmentReviews
        WHERE DoctorId = @DoctorId
    ),
    UpdatedAt = GETDATE()
    WHERE DoctorId = @DoctorId;
END