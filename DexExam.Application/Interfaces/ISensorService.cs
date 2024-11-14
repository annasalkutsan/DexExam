using DexExam.Domain.Models;

namespace DexExam.Application.Interfaces
{
    public interface ISensorService
    {
        // CRUD для датчиков
        Task AddSensorAsync(Sensor sensor);  
        Task UpdateSensorAsync(Sensor sensor);  
        Task RemoveSensorAsync(Guid sensorId); 
        Task<Sensor> GetSensorByIdAsync(Guid sensorId);  

        // Методы для работы с показаниями
        Task AddReadingAsync(Sensor sensor, double temperature, double humidity, double batteryLevel); 
        Task<ICollection<Reading>> GetReadingsForSensorAsync(Guid sensorId);  // Получить все показания для датчика
        Task RemoveReadingAsync(Guid sensorId, Guid readingId);  

        // Периодическое обновление данных от датчиков
        Task PollSensorDataAsync();  // Получить новые данные с датчиков

        // Логика обработки показаний (например, уведомления)
        Task ProcessSensorReadingAsync(Sensor sensor, Reading newReading);  // Обработать показания датчика
    }
}