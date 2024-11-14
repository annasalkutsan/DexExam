using DexExam.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DexExam.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly INotificationService _notificationService;

    public UserController(IUserService userService, INotificationService notificationService)
    {
        _userService = userService;
        _notificationService = notificationService;
    }

    // Получить пользователя по ID
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    // Получить уведомления пользователя
    [HttpGet("{userId}/notifications")]
    public async Task<IActionResult> GetUserNotificationsAsync(Guid userId)
    {
        var notifications = await _notificationService.GetNotificationsForUserAsync(userId);
        if (notifications == null || notifications.Count == 0) return NotFound();
        return Ok(notifications);
    }
}