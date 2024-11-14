using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DexExam.API.Controllers;

/// <summary>
/// Контроллер для работы с пользователями
/// </summary>
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

    /// <summary>
    /// Получить пользователя по ID
    /// </summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null) return NotFound();
        return Ok(user);
    }
        
    /// <summary>
    /// Получить уведомления пользователя
    /// </summary>
    [HttpGet("{userId}/notifications")]
    public async Task<IActionResult> GetUserNotificationsAsync(Guid userId)
    {
        var notifications = await _notificationService.GetNotificationsForUserAsync(userId);
        if (notifications == null || notifications.Count == 0) return NotFound();
        return Ok(notifications);
    }

    /// <summary>
    /// Создать нового пользователя
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] User user)
    {
        var newUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserByIdAsync), new { userId = newUser.Id }, newUser);
    }

    /// <summary>
    /// Обновить информацию о пользователе
    /// </summary>
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUserAsync(Guid userId, [FromBody] User updatedUser)
    {
        var user = await _userService.UpdateUserAsync(userId, updatedUser);
        if (user == null) return NotFound();
        return Ok(user);
    }

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUserAsync(Guid userId)
    {
        var success = await _userService.DeleteUserAsync(userId);
        if (!success) return NotFound();
        return NoContent();
    }
}