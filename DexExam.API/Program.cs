using DexExam.Infrastructure;
using DexExam.Application.Interfaces;
using DexExam.Application.Mapping;
using DexExam.Application.Services;
using DexExam.Domain.Interfaces;
using DexExam.Domain.Models;
using DexExam.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Подключение контроллеров
builder.Services.AddControllers();

// Swagger для документирования API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Настройка подключения к базе данных
var connectionString = builder.Configuration.GetConnectionString("DataBase");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);  
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Регистрация репозиториев
builder.Services.AddScoped<IRepository<Building>, Repository<Building>>();
builder.Services.AddScoped<IRepository<Notification>, Repository<Notification>>();
builder.Services.AddScoped<IRepository<Sensor>, Repository<Sensor>>();
builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<Reading>, Repository<Reading>>();

// Регистрация сервисов
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();