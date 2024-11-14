namespace DexExam.Domain.Models;
/// <summary>
/// Здание, в котором пользователь может размещать датчики
/// </summary>
public class Building
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }  // Навигационное свойство
    public string Name { get; set; }  // Название здания
    public string Address { get; set; }  // Адрес здания
    public string Description { get; set; } // Описание 
    public List<Sensor> Sensors { get; set; } = new List<Sensor>();
}