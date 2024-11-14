using DexExam.Domain.Models;

namespace DexExam.Application.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Получить пользователя по ID.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Пользователь с указанным ID.</returns>
        Task<User> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Получить уведомления пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список уведомлений для пользователя.</returns>
        Task<ICollection<Notification>> GetUserNotificationsAsync(Guid userId);
        
        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        Task<User> CreateUserAsync(User user);
        
        /// <summary>
        /// Обновить информацию о пользователе
        /// </summary>
        Task<User> UpdateUserAsync(Guid userId, User updatedUser);
        
        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<bool> DeleteUserAsync(Guid userId);
    }
}