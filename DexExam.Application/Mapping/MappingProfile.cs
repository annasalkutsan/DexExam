using AutoMapper;
using DexExam.Domain.Models;
using DexExam.Application.DTOs.User;
using DexExam.Application.DTOs.Building;
using DexExam.Application.DTOs.Sensor;
using DexExam.Application.DTOs.Notification;
using DexExam.Application.DTOs.Reading;

namespace DexExam.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Маппинг для User
            CreateMap<User, UserResponseDto>();

            // Маппинг для создания пользователя
            CreateMap<UserRequestDto, User>();

            // Маппинг для Building
            CreateMap<Building, BuildingResponseDto>();

            // Маппинг для создания здания
            CreateMap<BuildingRequestDto, Building>();

            // Маппинг для Sensor
            CreateMap<Sensor, SensorResponseDto>()
                .ForMember(dest => dest.BatteryLevel, opt => opt.MapFrom(src => src.BatteryLevel));

            // Маппинг для создания датчика
            CreateMap<SensorRequestDto, Sensor>();

            // Маппинг для Notification
            CreateMap<Notification, NotificationResponseDto>();

            // Маппинг для создания уведомления
            CreateMap<NotificationRequestDto, Notification>();

            // Маппинг для Reading
            CreateMap<Reading, ReadingResponseDto>();

            // Маппинг для создания показания
            CreateMap<ReadingRequestDto, Reading>();
        }
    }
}