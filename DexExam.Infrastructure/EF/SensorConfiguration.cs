using DexExam.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DexExam.Infrastructure.EF
{
    public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ToTable("sensors");
            
            builder.Property(s => s.Id)
                .HasColumnName("id");

            builder.HasKey(s => s.Id); 
            
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");

            builder.Property(s => s.Description)
                .HasMaxLength(500)
                .HasColumnName("description");

            builder.Property(s => s.GeoCoordinates)
                .HasMaxLength(150)
                .HasColumnName("geo_coordinates");

            builder.Property(s => s.PhotoUrl)
                .HasMaxLength(200)
                .HasColumnName("photo_url");

            builder.Property(s => s.MinThreshold)
                .IsRequired()
                .HasColumnName("min_threshold");

            builder.Property(s => s.MaxThreshold)
                .IsRequired()
                .HasColumnName("max_threshold");

            builder.Property(s => s.BatteryLevel)
                .IsRequired()
                .HasColumnName("battery_level");

            // связь с зданием
            builder.HasOne(s => s.Building) // Каждый датчик относится к одному зданию
                .WithMany(b => b.Sensors) // Здание может иметь много датчиков
                .HasForeignKey(s => s.BuildingId) 
                .OnDelete(DeleteBehavior.Cascade); 

            // связь с показаниями
            builder.HasMany(s => s.Readings) // Датчик может иметь много показаний
                .WithOne(r => r.Sensor) // Каждое показание относится к одному датчику
                .HasForeignKey(r => r.SensorId) 
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}