using DexExam.Application.DTOs.Building;
using DexExam.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DexExam.API.Controllers
{
    [ApiController]
    [Route("api/buildings")]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBuildingsAsync(Guid userId)
        {
            var buildings = await _buildingService.GetUserBuildingsAsync(userId);
            return Ok(buildings);
        }

        [HttpPost("user/{userId}")]
        public async Task<IActionResult> AddBuildingAsync(Guid userId, [FromBody] BuildingRequestDto buildingDto)
        {
            await _buildingService.AddBuildingAsync(userId, buildingDto);
            return CreatedAtAction(nameof(GetUserBuildingsAsync), new { userId = userId }, buildingDto);
        }

        [HttpDelete("user/{userId}/{buildingId}")]
        public async Task<IActionResult> RemoveBuildingAsync(Guid userId, Guid buildingId)
        {
            await _buildingService.RemoveBuildingAsync(userId, buildingId);
            return NoContent();
        }

        [HttpPut("user/{userId}/{buildingId}")]
        public async Task<IActionResult> UpdateBuildingAsync(Guid userId, Guid buildingId, [FromBody] BuildingRequestDto updatedBuildingDto)
        {
            var building = await _buildingService.UpdateBuildingAsync(userId, buildingId, updatedBuildingDto);
            if (building == null) return NotFound();
            return Ok(building);
        }
    }
}