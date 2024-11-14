using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using TgBotGuide.Domain.Interfaces;

namespace DexExam.Application.Services
{
    /// <summary>
    /// Реализация сервиса для работы с зданиями
    /// </summary>
    public class BuildingService : IBuildingService
    {
        private readonly IRepository<Building> _buildingRepository;

        public BuildingService(IRepository<Building> buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        /// <summary>
        /// Получить список зданий пользователя
        /// </summary>
        public async Task<ICollection<Building>> GetUserBuildingsAsync(Guid userId)
        {
            var buildings = await _buildingRepository.FindAsync(b => b.UserId == userId);
            return buildings;
        }

        /// <summary>
        /// Добавить новое здание
        /// </summary>
        public async Task AddBuildingAsync(Guid userId, Building building)
        {
            building.UserId = userId;
            await _buildingRepository.AddAsync(building);
        }

        /// <summary>
        /// Удалить здание
        /// </summary>
        public async Task RemoveBuildingAsync(Guid userId, Guid buildingId)
        {
            var building = await _buildingRepository.GetByIdAsync(buildingId);
            if (building != null && building.UserId == userId)
            {
                await _buildingRepository.RemoveAsync(building);
            }
        }

        /// <summary>
        /// Обновить данные здания
        /// </summary>
        public async Task<Building> UpdateBuildingAsync(Guid userId, Guid buildingId, Building updatedBuilding)
        {
            var building = await _buildingRepository.GetByIdAsync(buildingId);
            if (building == null || building.UserId != userId)
                return null;

            building.Name = updatedBuilding.Name;
            building.Address = updatedBuilding.Address;
            building.Description = updatedBuilding.Description;
            await _buildingRepository.UpdateAsync(building);
            return building;
        }
    }
}
