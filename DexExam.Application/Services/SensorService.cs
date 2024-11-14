using DexExam.Application.Interfaces;
using DexExam.Domain.Models;
using TgBotGuide.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexExam.Application.Services
{
    public class SensorService : ISensorService
    {
        private readonly IRepository<Sensor> _sensorRepository;
        private readonly IRepository<Reading> _readingRepository;
        private readonly INotificationService _notificationService;

        public SensorService(IRepository<Sensor> sensorRepository, IRepository<Reading> readingRepository, INotificationService notificationService)
        {
            _sensorRepository = sensorRepository;
            _readingRepository = readingRepository;
            _notificationService = notificationService;
        }

        // CRUD для датчиков
        public async Task AddSensorAsync(Sensor sensor)
        {
            await _sensorRepository.AddAsync(sensor);
        }

        public async Task UpdateSensorAsync(Sensor sensor)
        {
            await _sensorRepository.UpdateAsync(sensor);
        }

        public async Task RemoveSensorAsync(Guid sensorId)
        {
            var sensor = await _sensorRepository.GetByIdAsync(sensorId);
            if (sensor != null)
            {
                await _sensorRepository.RemoveAsync(sensor);
            }
        }

        public async Task<Sensor> GetSensorByIdAsync(Guid sensorId)
        {
            return await _sensorRepository.GetByIdAsync(sensorId);
        }

        // Методы для работы с показаниями
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

            // Сохраняем показание
            await _readingRepository.AddAsync(newReading);

            // Обрабатываем показание (например, отправляем уведомления)
            await ProcessSensorReadingAsync(sensor, newReading);
        }

        public async Task<ICollection<Reading>> GetReadingsForSensorAsync(Guid sensorId)
        {
            return await _readingRepository.FindAsync(r => r.SensorId == sensorId);
        }

        public async Task RemoveReadingAsync(Guid sensorId, Guid readingId)
        {
            var reading = await _readingRepository.GetByIdAsync(readingId);
            if (reading != null && reading.SensorId == sensorId)
            {
                await _readingRepository.RemoveAsync(reading);
            }
        }

        // Логика обработки показания (например, отправка уведомлений)
        public async Task ProcessSensorReadingAsync(Sensor sensor, Reading newReading)
        {
            // Проверка на температурный порог
            if (newReading.Temperature < sensor.MinThreshold || newReading.Temperature > sensor.MaxThreshold)
            {
                await _notificationService.NotifyUserAsync(sensor.Building.UserId, 
                    $"Температура на датчике {sensor.Name} выходит за пределы диапазона: {newReading.Temperature}°C.");
            }

            // Проверка на уровень заряда батареи
            if (newReading.BatteryLevel < 10)
            {
                await _notificationService.NotifyUserAsync(sensor.Building.UserId, 
                    $"Уровень заряда батареи на датчике {sensor.Name} ниже 10%.");
            }
        }

        // Периодическое обновление данных от датчиков (например, каждый час)
        public async Task PollSensorDataAsync()
        {
            var sensors = await _sensorRepository.GetAllAsync(); // Получаем все датчики
            foreach (var sensor in sensors)
            {
                // Имитируем получение новых данных с датчиков (замените на реальную логику)
                var temperature = GetSensorTemperature(sensor);
                var humidity = GetSensorHumidity(sensor);
                var batteryLevel = GetSensorBatteryLevel(sensor);

                // Добавляем показания
                await AddReadingAsync(sensor, temperature, humidity, batteryLevel);
            }
        }

        // Эмуляция получения данных с датчика (замените на реальную логику)
        private double GetSensorTemperature(Sensor sensor)
        {
            // Логика получения температуры с датчика
            return 22.5; // Пример
        }

        private double GetSensorHumidity(Sensor sensor)
        {
            // Логика получения влажности с датчика
            return 60.0; // Пример
        }

        private double GetSensorBatteryLevel(Sensor sensor)
        {
            // Логика получения уровня батареи с датчика
            return 50.0; // Пример
        }
    }
}
