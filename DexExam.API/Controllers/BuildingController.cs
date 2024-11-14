using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DexExam.API.Controllers;

[ApiController]
[Route("api/buildings")]
public class BuildingController : ControllerBase
{
    private readonly IBuildingService _buildingService;

    public BuildingController(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    // Получить список зданий пользователя
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserBuildingsAsync(Guid userId)
    {
        var buildings = await _buildingService.GetUserBuildingsAsync(userId);
        return Ok(buildings);
    }

    // Добавить новое здание
    [HttpPost("user/{userId}")]
    public async Task<IActionResult> AddBuildingAsync(Guid userId, [FromBody] Building building)
    {
        await _buildingService.AddBuildingAsync(userId, building);
        return CreatedAtAction(nameof(GetUserBuildingsAsync), new { userId = userId }, building);
    }

    // Удалить здание
    [HttpDelete("user/{userId}/{buildingId}")]
    public async Task<IActionResult> RemoveBuildingAsync(Guid userId, Guid buildingId)
    {
        await _buildingService.RemoveBuildingAsync(userId, buildingId);
        return NoContent();
    }
}