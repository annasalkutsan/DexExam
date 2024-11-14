using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using TgBotGuide.Domain.Interfaces;

namespace DexExam.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Notification> _notificationRepository;

    public UserService(IRepository<User> userRepository, IRepository<Notification> notificationRepository)
    {
        _userRepository = userRepository;
        _notificationRepository = notificationRepository;
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task<ICollection<Notification>> GetUserNotificationsAsync(Guid userId)
    {
        return await _notificationRepository.FindAsync(n => n.UserId == userId);
    }
}
