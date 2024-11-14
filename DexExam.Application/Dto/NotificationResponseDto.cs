namespace DexExam.Application.DTOs.Notification
{
    public class NotificationResponseDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}