using DexExam.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DexExam.API.Controllers
{
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

        // Получить уведомление по ID
        [HttpGet("{notificationId}")]
        public async Task<IActionResult> GetNotificationByIdAsync(Guid notificationId)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(notificationId);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        // Создать уведомление (отправить пользователю)
        [HttpPost("user/{userId}")]
        public async Task<IActionResult> NotifyUserAsync(Guid userId, [FromBody] string message)
        {
            await _notificationService.NotifyUserAsync(userId, message);
            return Ok();
        }

        // Обновить уведомление
        [HttpPut("{notificationId}")]
        public async Task<IActionResult> UpdateNotificationAsync(Guid notificationId, [FromBody] string newMessage)
        {
            await _notificationService.UpdateNotificationAsync(notificationId, newMessage);
            return NoContent(); // Успешно обновлено
        }

        // Удалить уведомление
        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> RemoveNotificationAsync(Guid notificationId)
        {
            await _notificationService.RemoveNotificationAsync(notificationId);
            return NoContent(); // Успешно удалено
        }
    }
}
