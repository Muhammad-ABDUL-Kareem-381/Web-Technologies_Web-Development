CREATE PROCEDURE sp_GetDoctorAvailableSlots
    @DoctorId INT,
    @Date DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DayOfWeek INT = DATEPART(WEEKDAY, @Date) - 1;

    SELECT 
        da.StartTime,
        da.EndTime,
        da.SlotDuration
    FROM DoctorAvailability da
    WHERE da.DoctorId = @DoctorId
      AND da.DayOfWeek = @DayOfWeek
      AND da.IsActive = 1
      AND NOT EXISTS (
          SELECT 1 FROM DoctorSpecialDates dsd
          WHERE dsd.DoctorId = @DoctorId
            AND dsd.SpecialDate = @Date
            AND dsd.IsAvailable = 0
      );
END