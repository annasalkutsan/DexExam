using DexExam.Domain.Models;

namespace DexExam.Application.Interfaces;

public interface ISensorService
{
    Task AddReadingAsync(Sensor sensor, double temperature, double humidity, double batteryLevel);
    Task ProcessSensorReadingAsync(Sensor sensor, Reading newReading);
    Task<Sensor> GetSensorByIdAsync(Guid sensorId);
}