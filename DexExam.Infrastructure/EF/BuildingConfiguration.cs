using DexExam.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DexExam.Infrastructure.EF;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable("buildings");

        builder.Property(b => b.Id)
            .HasColumnName("id");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Name)
            .HasMaxLength(100)
            .HasColumnName("name");

        builder.Property(b => b.Address)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("address");

        builder.Property(b => b.Description)
            .HasMaxLength(500)
            .HasColumnName("description");

        // связь с пользователем
        builder.HasOne(b => b.User) // Каждое здание относится к одному пользователю
            .WithMany(u => u.Buildings) // Один пользователь может иметь несколько зданий
            .HasForeignKey(b => b.UserId) 
            .OnDelete(DeleteBehavior.SetNull); 

        //  связь с датчиками
        builder.HasMany(b => b.Sensors) // Здание может иметь много датчиков
            .WithOne(s => s.Building) // Каждый датчик относится к одному зданию
            .HasForeignKey(s => s.BuildingId) 
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
