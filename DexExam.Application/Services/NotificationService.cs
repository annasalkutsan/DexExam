using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using TgBotGuide.Domain.Interfaces;

namespace DexExam.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IRepository<Notification> _notificationRepository;

    public NotificationService(IRepository<Notification> notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }
    
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
}