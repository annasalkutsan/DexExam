using DexExam.Application.DTOs.Building;

namespace DexExam.Application.Interfaces
{
    public interface IBuildingService
    { 
        Task<ICollection<BuildingResponseDto>> GetUserBuildingsAsync(Guid userId); // Получить список зданий пользователя
        Task AddBuildingAsync(Guid userId, BuildingRequestDto building);// Добавить новое здание
        Task RemoveBuildingAsync(Guid userId, Guid buildingId);// Удалить здание
        Task<BuildingResponseDto> UpdateBuildingAsync(Guid userId, Guid buildingId, BuildingRequestDto updatedBuilding);// Обновить данные здания
    }
}