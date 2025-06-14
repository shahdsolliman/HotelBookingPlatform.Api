using HotelBookingPlatform.Application.Services;
using HotelBookingPlatform.Application.Services.Contract;
using HotelBookingPlatform.Application.Services.Helper;
using HotelBookingPlatform.Core;
using HotelBookingPlatform.Core.Entities;
using HotelBookingPlatform.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelBookingPlatform.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Repository<User>().GetAllAsync();
            return ServiceResult<IEnumerable<User>>.Success(users);
        }

        public async Task<ServiceResult<User>> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (user == null)
                return ServiceResult<User>.Failure();

            return ServiceResult<User>.Success(user);
        }

        public async Task<ServiceResult<User>> CreateUserAsync(User user)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(user.Name))
                return ServiceResult<User>.Failure();

            if (!IsValidEmail(user.Email))
                return ServiceResult<User>.Failure();

            if (user.Age <= 0)
                return ServiceResult<User>.Failure();

            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return ServiceResult<User>.Success(user);
        }

        public async Task<ServiceResult<User>> UpdateUserAsync(Guid id, User updatedUser)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (user == null)
                return ServiceResult<User>.Failure();

            // Validation
            if (string.IsNullOrWhiteSpace(updatedUser.Name))
                return ServiceResult<User>.Failure();

            if (!IsValidEmail(updatedUser.Email))
                return ServiceResult<User>.Failure();

            if (updatedUser.Age <= 0)
                return ServiceResult<User>.Failure();

            // Update fields
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Age = updatedUser.Age;

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.CompleteAsync();

            return ServiceResult<User>.Success(user);
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(Guid id)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (user == null)
                return ServiceResult<bool>.Failure();

            _unitOfWork.Repository<User>().Delete(user);
            await _unitOfWork.CompleteAsync();

            return ServiceResult<bool>.Success(true);
        }

        private bool IsValidEmail(string email)
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
