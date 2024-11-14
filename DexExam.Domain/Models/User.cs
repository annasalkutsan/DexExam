namespace DexExam.Domain.Models;
/// <summary>
/// Пользователь, который может добавлять здания и управлять датчиками
/// </summary>
public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password{ get; set; }
    public List<Building> Buildings { get; set; } = new List<Building>();
    public List<Notification> Notifications { get; set; } = new List<Notification>();
}