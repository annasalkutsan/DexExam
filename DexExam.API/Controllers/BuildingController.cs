using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DexExam.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с зданиями
    /// </summary>
    [ApiController]
    [Route("api/buildings")]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        /// <summary>
        /// Получить список зданий пользователя
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBuildingsAsync(Guid userId)
        {
            var buildings = await _buildingService.GetUserBuildingsAsync(userId);
            return Ok(buildings);
        }

        /// <summary>
        /// Добавить новое здание
        /// </summary>
        [HttpPost("user/{userId}")]
        public async Task<IActionResult> AddBuildingAsync(Guid userId, [FromBody] Building building)
        {
            await _buildingService.AddBuildingAsync(userId, building);
            return CreatedAtAction(nameof(GetUserBuildingsAsync), new { userId = userId }, building);
        }

        /// <summary>
        /// Удалить здание
        /// </summary>
        [HttpDelete("user/{userId}/{buildingId}")]
        public async Task<IActionResult> RemoveBuildingAsync(Guid userId, Guid buildingId)
        {
            await _buildingService.RemoveBuildingAsync(userId, buildingId);
            return NoContent();
        }

        /// <summary>
        /// Обновить данные здания
        /// </summary>
        [HttpPut("user/{userId}/{buildingId}")]
        public async Task<IActionResult> UpdateBuildingAsync(Guid userId, Guid buildingId, [FromBody] Building updatedBuilding)
        {
            var building = await _buildingService.UpdateBuildingAsync(userId, buildingId, updatedBuilding);
            if (building == null) return NotFound();
            return Ok(building);
        }
    }
}