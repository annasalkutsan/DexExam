using DexExam.Application.Interfaces;
using DexExam.Domain.Interfaces;
using DexExam.Domain.Models;

namespace DexExam.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> _notificationRepository;

        public NotificationService(IRepository<Notification> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        // Создание уведомления
        public async Task NotifyUserAsync(Guid userId, string message)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            // Добавляем уведомление в базу данных
            await _notificationRepository.AddAsync(notification);
        }

        // Получение уведомлений для пользователя
        public async Task<ICollection<Notification>> GetNotificationsForUserAsync(Guid userId)
        {
            var notifications = await _notificationRepository.FindAsync(n => n.UserId == userId);
            return notifications;
        }

        // Получить уведомление по ID
        public async Task<Notification> GetNotificationByIdAsync(Guid notificationId)
        {
            return await _notificationRepository.GetByIdAsync(notificationId);
        }

        // Обновление уведомления
        public async Task UpdateNotificationAsync(Guid notificationId, string newMessage)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            if (notification != null)
            {
                notification.Message = newMessage;
                notification.Timestamp = DateTime.UtcNow; // Обновляем время уведомления
                await _notificationRepository.UpdateAsync(notification);
            }
        }

        // Удаление уведомления
        public async Task RemoveNotificationAsync(Guid notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            if (notification != null)
            {
                await _notificationRepository.RemoveAsync(notification);
            }
        }
    }
}
