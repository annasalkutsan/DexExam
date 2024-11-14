using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using TgBotGuide.Domain.Interfaces;

namespace DexExam.Application.Services;

public class BuildingService : IBuildingService
{
    private readonly IRepository<Building> _buildingRepository;

    public BuildingService(IRepository<Building> buildingRepository)
    {
        _buildingRepository = buildingRepository;
    }

    public async Task<ICollection<Building>> GetUserBuildingsAsync(Guid userId)
    {
        var user = await _buildingRepository.FindAsync(b => b.UserId == userId);
        return user;
    }

    public async Task AddBuildingAsync(Guid userId, Building building)
    {
        building.UserId = userId;
        await _buildingRepository.AddAsync(building);
    }

    public async Task RemoveBuildingAsync(Guid userId, Guid buildingId)
    {
        var building = await _buildingRepository.GetByIdAsync(buildingId);
        if (building != null && building.UserId == userId)
        {
            await _buildingRepository.RemoveAsync(building);
        }
    }
}
