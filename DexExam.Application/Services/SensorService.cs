using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using TgBotGuide.Domain.Interfaces;

namespace DexExam.Application.Services;
public class SensorService : ISensorService
{
    private readonly IRepository<Sensor> _sensorRepository;  // Репозиторий для работы с датчиками
    private readonly INotificationService _notificationService;  // Сервис для отправки уведомлений

    public SensorService(IRepository<Sensor> sensorRepository, INotificationService notificationService)
    {
        _sensorRepository = sensorRepository;
        _notificationService = notificationService;
    }

    public async Task<Sensor> GetSensorByIdAsync(Guid sensorId)
    {
        return await _sensorRepository.GetByIdAsync(sensorId);
    }
    
    public async Task AddReadingAsync(Sensor sensor, double temperature, double humidity, double batteryLevel)
    {
        var newReading = new Reading
        {
            SensorId = sensor.Id,
            Temperature = temperature,
            Humidity = humidity,
            BatteryLevel = batteryLevel,
            Timestamp = DateTime.UtcNow
        };
        await ProcessSensorReadingAsync(sensor, newReading);  // Обработка показания
    }

    public async Task ProcessSensorReadingAsync(Sensor sensor, Reading newReading)
    {
        // Логика обработки датчика

        // Если температура выходит за пределы, отправляем уведомление
        if (newReading.Temperature < sensor.MinThreshold || newReading.Temperature > sensor.MaxThreshold)
        {
            await _notificationService.NotifyUserAsync(sensor.Building.UserId, 
                $"Температура на датчике {sensor.Name} выходит за пределы диапазона: {newReading.Temperature}°C.");
        }

        // Если батарея на датчике имеет низкий заряд, отправляем уведомление
        if (newReading.BatteryLevel < 10)
        {
            await _notificationService.NotifyUserAsync(sensor.Building.UserId, 
                $"Уровень заряда батареи на датчике {sensor.Name} ниже 10%.");
        }
    }
}