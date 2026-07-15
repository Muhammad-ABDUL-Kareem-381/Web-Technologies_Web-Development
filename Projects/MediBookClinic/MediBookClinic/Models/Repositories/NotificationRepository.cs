using Dapper;
using MediBookClinic.Data;
using MediBookClinic.Models.Entities;
using MediBookClinic.Models.Interfaces.Repository;

namespace MediBookClinic.Models.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DapperContext _context;

        public NotificationRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Notification> CreateAsync(Notification notification)
        {
            var sql = @"INSERT INTO Notifications (UserId, Title, Message, Type, Channel, IsRead, IsSent, SentAt, RelatedEntityType, RelatedEntityId, CreatedAt)
                      VALUES (@UserId, @Title, @Message, @Type, @Channel, @IsRead, @IsSent, @SentAt, @RelatedEntityType, @RelatedEntityId, @CreatedAt);
                      SELECT CAST(SCOPE_IDENTITY() as int);";
            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, notification);
            notification.NotificationId = id;
            return notification;
        }

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(string userId)
        {
            var sql = "SELECT * FROM Notifications WHERE UserId = @UserId ORDER BY CreatedAt DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Notification>(sql, new { UserId = userId });
        }

        public async Task<IEnumerable<Notification>> GetUnreadAsync(string userId)
        {
            var sql = "SELECT * FROM Notifications WHERE UserId = @UserId AND IsRead = 0 ORDER BY CreatedAt DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Notification>(sql, new { UserId = userId });
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            var sql = "SELECT COUNT(*) FROM Notifications WHERE UserId = @UserId AND IsRead = 0";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, new { UserId = userId });
        }

        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            var sql = "UPDATE Notifications SET IsRead = 1 WHERE NotificationId = @NotificationId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new { NotificationId = notificationId });
            return affectedRows > 0;
        }

        public async Task<bool> MarkAllAsReadAsync(string userId)
        {
            var sql = "UPDATE Notifications IsRead = 1  WHERE UserId = @UserId AND IsRead = 0";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new { UserId = userId });
            return true;
        }

        public async Task<bool> DeleteAsync(int notificationId)
        {
            var sql = "DELETE FROM Notifications WHERE NotificationId = @NotificationId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new { NotificationId = notificationId });
            return affectedRows > 0;
        }
    }
}
