using DexExam.Domain.Models;

namespace DexExam.Application.Interfaces
{
    public interface INotificationService
    {
        Task NotifyUserAsync(Guid userId, string message);  // Уведомление для пользователя
        Task<ICollection<Notification>> GetNotificationsForUserAsync(Guid userId);  // Получить уведомления для пользователя
        Task<Notification> GetNotificationByIdAsync(Guid notificationId);  // Получить уведомление по ID
        Task UpdateNotificationAsync(Guid notificationId, string newMessage);  // Обновить уведомление
        Task RemoveNotificationAsync(Guid notificationId);  // Удалить уведомление
    }
}