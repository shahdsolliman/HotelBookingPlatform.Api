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
                return ServiceResult<User>.Failure("User not found.");

            return ServiceResult<User>.Success(user);
        }

        public async Task<ServiceResult<User>> CreateUserAsync(User user)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(user.Name))
                return ServiceResult<User>.Failure("Name is required.");

            if (!IsValidEmail(user.Email))
                return ServiceResult<User>.Failure("Invalid email format.");

            if (user.Age <= 0)
                return ServiceResult<User>.Failure("Age must be greater than 0.");

            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return ServiceResult<User>.Success(user);
        }

        public async Task<ServiceResult<User>> UpdateUserAsync(Guid id, User updatedUser)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (user == null)
                return ServiceResult<User>.Failure("User not found.");

            // Validation
            if (string.IsNullOrWhiteSpace(updatedUser.Name))
                return ServiceResult<User>.Failure("Name is required.");

            if (!IsValidEmail(updatedUser.Email))
                return ServiceResult<User>.Failure("Invalid email format.");

            if (updatedUser.Age <= 0)
                return ServiceResult<User>.Failure("Age must be greater than 0.");

            // Update fields
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Age = updatedUser.Age;

            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return ServiceResult<User>.Success(user);
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(Guid id)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (user == null)
                return ServiceResult<bool>.Failure("User not found.");

            await _unitOfWork.Repository<User>().DeleteAsync(user.Id);
            await _unitOfWork.CompleteAsync();

            return ServiceResult<bool>.Success(true, "User deleted successfully.");
        }

        private bool IsValidEmail(string email)
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
