INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES 
    (NEWID(), 'Admin', 'ADMIN', NEWID()),
    (NEWID(), 'Doctor', 'DOCTOR', NEWID()),
    (NEWID(), 'Patient', 'PATIENT', NEWID());