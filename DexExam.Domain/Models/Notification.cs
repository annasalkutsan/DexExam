namespace DexExam.Domain.Models;
/// <summary>
/// Уведомление
/// </summary>
public class Notification
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }  // Навигационное свойство
    public string Message { get; set; }  // Текст нотификации
    public DateTime Timestamp { get; set; }  // Время создания уведомления
}