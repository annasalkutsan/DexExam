using DexExam.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DexExam.API.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    // Получить уведомления пользователя
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetNotificationsAsync(Guid userId)
    {
        var notifications = await _notificationService.GetNotificationsForUserAsync(userId);
        return Ok(notifications);
    }
}