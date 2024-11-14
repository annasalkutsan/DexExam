using AutoMapper;
using DexExam.Application.DTOs.Building;
using DexExam.Application.Interfaces;
using DexExam.Domain.Interfaces;
using DexExam.Domain.Models;

namespace DexExam.Application.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IRepository<Building> _buildingRepository;
        private readonly IMapper _mapper;

        public BuildingService(IRepository<Building> buildingRepository, IMapper mapper)
        {
            _buildingRepository = buildingRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<BuildingResponseDto>> GetUserBuildingsAsync(Guid userId)
        {
            var buildings = await _buildingRepository.FindAsync(b => b.UserId == userId);
            return _mapper.Map<ICollection<BuildingResponseDto>>(buildings);
        }

        public async Task AddBuildingAsync(Guid userId, BuildingRequestDto buildingDto)
        {
            var building = _mapper.Map<Building>(buildingDto);
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

        public async Task<BuildingResponseDto> UpdateBuildingAsync(Guid userId, Guid buildingId, BuildingRequestDto updatedBuildingDto)
        {
            var building = await _buildingRepository.GetByIdAsync(buildingId);
            if (building == null || building.UserId != userId)
                return null;

            building.Name = updatedBuildingDto.Name;
            building.Address = updatedBuildingDto.Address;
            building.Description = updatedBuildingDto.Description;

            await _buildingRepository.UpdateAsync(building);
            return _mapper.Map<BuildingResponseDto>(building);
        }
    }
}