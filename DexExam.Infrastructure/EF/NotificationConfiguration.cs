using DexExam.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DexExam.Infrastructure.EF;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notifications");

        builder.Property(n => n.Id)
            .HasColumnName("id"); 

        builder.HasKey(n => n.Id); 
        
        builder.Property(n => n.Message)
            .IsRequired()
            .HasMaxLength(1000) 
            .HasColumnName("message");

        builder.Property(n => n.Timestamp)
            .IsRequired()
            .HasColumnName("timestamp"); 

        // связь с пользователем
        builder.HasOne(n => n.User) // Каждое уведомление относится к одному пользователю
            .WithMany(u => u.Notifications) // Пользователь может иметь много уведомлений
            .HasForeignKey(n => n.UserId) // Внешний ключ UserId
            .OnDelete(DeleteBehavior.Cascade); // При удалении пользователя, все его уведомления удаляются
    }
}