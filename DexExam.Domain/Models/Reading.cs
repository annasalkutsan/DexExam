namespace DexExam.Domain.Models;
/// <summary>
/// Показание датчика
/// </summary>
public class Reading
{
    public Guid Id { get; set; }
    public Guid SensorId { get; set; }
    public Sensor Sensor { get; set; }  // Навигационное свойство
    public DateTime Timestamp { get; set; }  // Время получения показания
    public double Temperature { get; set; }  // Температура
    public double Humidity { get; set; }  // Влажность
    public double BatteryLevel { get; set; }  // Уровень заряда батареи на момент отправки
}