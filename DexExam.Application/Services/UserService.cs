using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using TgBotGuide.Domain.Interfaces;

namespace DexExam.Application.Services
{
    /// <summary>
    /// Реализация сервиса для работы с пользователями
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Notification> _notificationRepository;

        public UserService(IRepository<User> userRepository, IRepository<Notification> notificationRepository)
        {
            _userRepository = userRepository;
            _notificationRepository = notificationRepository;
        }

        /// <summary>
        /// Получить пользователя по его идентификатору
        /// </summary>
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        /// <summary>
        /// Получить уведомления для пользователя
        /// </summary>
        public async Task<ICollection<Notification>> GetUserNotificationsAsync(Guid userId)
        {
            return await _notificationRepository.FindAsync(n => n.UserId == userId);
        }

        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        public async Task<User> CreateUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
            return user;
        }

        /// <summary>
        /// Обновить информацию о пользователе
        /// </summary>
        public async Task<User> UpdateUserAsync(Guid userId, User updatedUser)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return null;
            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            await _userRepository.UpdateAsync(user);
            return user;
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;
            await _userRepository.RemoveAsync(user);
            return true;
        }
    }
}
