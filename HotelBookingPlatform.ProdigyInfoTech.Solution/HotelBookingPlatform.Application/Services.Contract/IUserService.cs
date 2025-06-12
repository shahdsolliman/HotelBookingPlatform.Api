using HotelBookingPlatform.Application.Services.Helper;
using HotelBookingPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingPlatform.Application.Services.Contract
{
    public interface IUserService
    {
        Task<ServiceResult<IEnumerable<User>>> GetAllUsersAsync();
        Task<ServiceResult<User>> GetUserByIdAsync(Guid id);
        Task<ServiceResult<User>> CreateUserAsync(User user);
        Task<ServiceResult<User>> UpdateUserAsync(Guid id, User updatedUser);
        Task<ServiceResult<bool>> DeleteUserAsync(Guid id);
    }
}
