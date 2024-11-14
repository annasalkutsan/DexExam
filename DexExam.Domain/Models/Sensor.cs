namespace DexExam.Domain.Models;
/// <summary>
/// Датчик, который собирает данные о состоянии здания
/// </summary>
public class Sensor
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public Building Building { get; set; }  // Навигационное свойство
    public string Name { get; set; }  // Название датчика
    public string Description { get; set; }  // Описание датчика
    public string GeoCoordinates { get; set; }  // Географические координаты
    public string PhotoUrl { get; set; }  // Ссылка на фото датчика
    public double MinThreshold { get; set; }  // Минимальное значение (настраивается пользователем)
    public double MaxThreshold { get; set; }  // Максимальное значение (настраивается пользователем)
    public double BatteryLevel { get; set; }  // Уровень заряда батареи
    public List<Reading> Readings { get; set; } = new List<Reading>();
}
