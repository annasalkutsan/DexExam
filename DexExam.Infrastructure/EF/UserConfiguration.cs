using DexExam.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DexExam.Infrastructure.EF;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(u => u.Id)
            .HasColumnName("id");

        builder.HasKey(u => u.Id); 
        
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("username");

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName("email");

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName("password");

        // связь с таблицей Building
        builder.HasMany(u => u.Buildings) // Пользователь может иметь несколько зданий
            .WithOne(b => b.User) // Здание относится к одному пользователю
            .HasForeignKey(b => b.UserId) 
            .OnDelete(DeleteBehavior.SetNull); 
    }
}