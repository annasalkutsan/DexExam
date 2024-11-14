using DexExam.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DexExam.Infrastructure.EF;

public class ReadingConfiguration : IEntityTypeConfiguration<Reading>
{
    public void Configure(EntityTypeBuilder<Reading> builder)
    {
        builder.ToTable("readings");

        builder.Property(r => r.Id)
            .HasColumnName("id");

        builder.HasKey(r => r.Id); 

        builder.Property(r => r.Timestamp)
            .IsRequired()
            .HasColumnName("timestamp"); 

        builder.Property(r => r.Temperature)
            .IsRequired()
            .HasColumnName("temperature"); 

        builder.Property(r => r.Humidity)
            .IsRequired()
            .HasColumnName("humidity"); 
        
        builder.Property(r => r.BatteryLevel)
            .IsRequired()
            .HasColumnName("battery_level");

        // связь с датчиком
        builder.HasOne(r => r.Sensor) // Каждое показание относится к одному датчику
            .WithMany(s => s.Readings) // Датчик может иметь много показаний
            .HasForeignKey(r => r.SensorId) 
            .OnDelete(DeleteBehavior.Cascade);
    }
}