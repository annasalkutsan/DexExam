using DexExam.Domain.Models;

namespace DexExam.Application.Interfaces;

public interface IBuildingService
{
    Task<ICollection<Building>> GetUserBuildingsAsync(Guid userId);
    Task AddBuildingAsync(Guid userId, Building building);
    Task RemoveBuildingAsync(Guid userId, Guid buildingId);
}