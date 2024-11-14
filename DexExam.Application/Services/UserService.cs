using AutoMapper;
using DexExam.Application.DTOs.Notification;
using DexExam.Application.DTOs.User;
using DexExam.Domain.Models;
using DexExam.Application.Interfaces;
using DexExam.Domain.Interfaces;

namespace DexExam.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IRepository<Notification> notificationRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<ICollection<NotificationResponseDto>> GetUserNotificationsAsync(Guid userId)
        {
            var notifications = await _notificationRepository.FindAsync(n => n.UserId == userId);
            return _mapper.Map<ICollection<NotificationResponseDto>>(notifications);
        }

        public async Task<UserResponseDto> CreateUserAsync(UserRequestDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.AddAsync(user);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> UpdateUserAsync(Guid userId, UserRequestDto updatedUserDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return null;

            _mapper.Map(updatedUserDto, user); // обновляем поля объекта user
            await _userRepository.UpdateAsync(user);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;
            await _userRepository.RemoveAsync(user);
            return true;
        }
    }
}