using DexExam.Domain.Models;

namespace DexExam.Application.Interfaces
{
    public interface ISensorService
    {
        Task AddSensorAsync(Sensor sensor);  // Добавить датчик
        Task UpdateSensorAsync(Sensor sensor);  // Обновить датчик
        Task RemoveSensorAsync(Guid sensorId);  // Удалить датчик
        Task<Sensor> GetSensorByIdAsync(Guid sensorId);  // Получить датчик по ID
        
        // Методы для работы с показаниями
        Task AddReadingAsync(Sensor sensor, double temperature, double humidity, double batteryLevel);  // Добавить показание
        Task<ICollection<Reading>> GetReadingsForSensorAsync(Guid sensorId);  // Получить все показания для датчика
        Task RemoveReadingAsync(Guid sensorId, Guid readingId);  // Удалить показание для датчика
    }
}