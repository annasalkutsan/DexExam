using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DexExam.API.Controllers
{
    [ApiController]
    [Route("api/sensors")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        // Добавить новый датчик
        [HttpPost("building/{buildingId}")]
        public async Task<IActionResult> AddSensorAsync(Guid buildingId, [FromBody] Sensor sensor)
        {
            sensor.BuildingId = buildingId;
            await _sensorService.AddSensorAsync(sensor);
            return CreatedAtAction(nameof(GetSensorAsync), new { sensorId = sensor.Id }, sensor);
        }

        // Получить данные по датчику
        [HttpGet("{sensorId}")]
        public async Task<IActionResult> GetSensorAsync(Guid sensorId)
        {
            var sensor = await _sensorService.GetSensorByIdAsync(sensorId);
            if (sensor == null) return NotFound();
            return Ok(sensor);
        }

        // Обновить данные датчика
        [HttpPut("{sensorId}")]
        public async Task<IActionResult> UpdateSensorAsync(Guid sensorId, [FromBody] Sensor sensor)
        {
            var existingSensor = await _sensorService.GetSensorByIdAsync(sensorId);
            if (existingSensor == null) return NotFound();

            existingSensor.Name = sensor.Name;
            existingSensor.Description = sensor.Description;
            existingSensor.GeoCoordinates = sensor.GeoCoordinates;
            existingSensor.PhotoUrl = sensor.PhotoUrl;
            existingSensor.MinThreshold = sensor.MinThreshold;
            existingSensor.MaxThreshold = sensor.MaxThreshold;
            existingSensor.BatteryLevel = sensor.BatteryLevel;

            await _sensorService.UpdateSensorAsync(existingSensor);
            return NoContent();
        }

        // Удалить датчик
        [HttpDelete("{sensorId}")]
        public async Task<IActionResult> RemoveSensorAsync(Guid sensorId)
        {
            await _sensorService.RemoveSensorAsync(sensorId);
            return NoContent();
        }

        // Добавить показания датчика
        [HttpPost("{sensorId}/readings")]
        public async Task<IActionResult> AddReadingAsync(Guid sensorId, [FromBody] Reading reading)
        {
            var sensor = await _sensorService.GetSensorByIdAsync(sensorId);
            if (sensor == null) return NotFound();

            await _sensorService.AddReadingAsync(sensor, reading.Temperature, reading.Humidity, reading.BatteryLevel);
            return CreatedAtAction(nameof(GetReadingsForSensorAsync), new { sensorId }, reading);
        }

        // Получить показания для датчика
        [HttpGet("{sensorId}/readings")]
        public async Task<IActionResult> GetReadingsForSensorAsync(Guid sensorId)
        {
            var readings = await _sensorService.GetReadingsForSensorAsync(sensorId);
            return Ok(readings);
        }

        // Удалить показания датчика
        [HttpDelete("{sensorId}/readings/{readingId}")]
        public async Task<IActionResult> RemoveReadingAsync(Guid sensorId, Guid readingId)
        {
            await _sensorService.RemoveReadingAsync(sensorId, readingId);
            return NoContent();
        }
    }
}
