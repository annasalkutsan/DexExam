using AutoMapper;
using DexExam.Application.DTOs.Notification;
using DexExam.Application.DTOs.User;
using DexExam.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DexExam.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, INotificationService notificationService, IMapper mapper)
        {
            _userService = userService;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserResponseDto>(user));
        }

        [HttpGet("{userId}/notifications")]
        public async Task<IActionResult> GetUserNotificationsAsync(Guid userId)
        {
            var notifications = await _notificationService.GetNotificationsForUserAsync(userId);
            if (notifications == null || notifications.Count == 0) return NotFound();
            return Ok(_mapper.Map<ICollection<NotificationResponseDto>>(notifications));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRequestDto userDto)
        {
            var newUser = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUserByIdAsync), new { userId = newUser.Id }, _mapper.Map<UserResponseDto>(newUser));
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, [FromBody] UserRequestDto updatedUserDto)
        {
            var user = await _userService.UpdateUserAsync(userId, updatedUserDto);
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserResponseDto>(user));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            var success = await _userService.DeleteUserAsync(userId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
