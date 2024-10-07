
namespace HealtSync.Domain.Entities.System
{
    public class Notifications
    {
        public int NotificationId { get; set; }
        public int UserID { get; set; }
        public string? Message { get; set; }
        public DateTime? SentAt { get; set; }


    }
}
