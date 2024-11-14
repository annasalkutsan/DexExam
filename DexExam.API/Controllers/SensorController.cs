using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DexExam.API.Controllers;

[ApiController]
[Route("api/sensors")]
public class SensorController : ControllerBase
{
    private readonly ISensorService _sensorService;

    public SensorController(ISensorService sensorService)
    {
        _sensorService = sensorService;
    }

    // Добавить показания датчика
    [HttpPost("{sensorId}/readings")]
    public async Task<IActionResult> AddReadingAsync(Guid sensorId, [FromBody] Reading reading)
    {
        var sensor = await _sensorService.GetSensorByIdAsync(sensorId); // Предполагается, что есть метод для получения датчика
        if (sensor == null) return NotFound();
            
        await _sensorService.AddReadingAsync(sensor, reading.Temperature, reading.Humidity, reading.BatteryLevel);
        return Ok();
    }

    // Получить данные по датчику
    [HttpGet("{sensorId}")]
    public async Task<IActionResult> GetSensorAsync(Guid sensorId)
    {
        var sensor = await _sensorService.GetSensorByIdAsync(sensorId); // Предполагается, что есть метод для получения датчика
        if (sensor == null) return NotFound();
            
        return Ok(sensor);
    }
}