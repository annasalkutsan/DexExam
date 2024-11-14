using DexExam.Application.DTOs.Notification;
using DexExam.Application.DTOs.User;

namespace DexExam.Application.Interfaces
{
    public interface IUserService
    {
        // CRUD для пользователей
        Task<UserResponseDto> GetUserByIdAsync(Guid userId);
        Task<ICollection<NotificationResponseDto>> GetUserNotificationsAsync(Guid userId);
        Task<UserResponseDto> CreateUserAsync(UserRequestDto userDto);
        Task<UserResponseDto> UpdateUserAsync(Guid userId, UserRequestDto updatedUserDto);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}