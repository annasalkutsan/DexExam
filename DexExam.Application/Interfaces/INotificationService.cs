using DexExam.Domain.Models;

namespace DexExam.Application.Interfaces;

public interface INotificationService
{
    Task NotifyUserAsync(Guid userId, string message);
}
    