using DexExam.Domain.Models;

namespace DexExam.Application.Interfaces
{
    /// <summary>
    /// Сервис для работы с зданиями
    /// </summary>
    public interface IBuildingService
    {
        /// <summary>
        /// Получить список зданий пользователя
        /// </summary>
        Task<ICollection<Building>> GetUserBuildingsAsync(Guid userId);
        
        /// <summary>
        /// Добавить новое здание
        /// </summary>
        Task AddBuildingAsync(Guid userId, Building building);
        
        /// <summary>
        /// Удалить здание
        /// </summary>
        Task RemoveBuildingAsync(Guid userId, Guid buildingId);

        /// <summary>
        /// Обновить данные здания
        /// </summary>
        Task<Building> UpdateBuildingAsync(Guid userId, Guid buildingId, Building updatedBuilding);
    }
}