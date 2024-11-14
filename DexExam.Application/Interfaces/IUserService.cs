using DexExam.Domain.Models;

namespace DexExam.Application.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(Guid userId);
    Task<ICollection<Notification>> GetUserNotificationsAsync(Guid userId);
}