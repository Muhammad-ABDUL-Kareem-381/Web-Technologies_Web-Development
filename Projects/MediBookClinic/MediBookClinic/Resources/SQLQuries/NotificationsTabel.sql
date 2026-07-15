CREATE TABLE Notifications (
        NotificationId INT IDENTITY(1,1) PRIMARY KEY,
        UserId NVARCHAR(450) NOT NULL,
        Title NVARCHAR(200) NOT NULL,
        Message NVARCHAR(MAX) NOT NULL,
        Type NVARCHAR(50) NOT NULL,
        Channel NVARCHAR(50) NOT NULL,
        IsRead BIT DEFAULT 0,
        IsSent BIT DEFAULT 0,
        SentAt DATETIME2,
        RelatedEntityType NVARCHAR(50),
        RelatedEntityId INT,
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);