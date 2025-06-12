using HotelBookingPlatform.Application.Services.Contract;
using HotelBookingPlatform.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnapShop.API.Errors;

namespace HotelBookingPlatform.APIs.Controllers
{
    public class UsersController : BaseAPIController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]   //GET: /api/users
        public async Task<ActionResult<IReadOnlyList<User>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new ApiResponse(400));
        }

        [HttpGet("{id}")]  //GET: api/users/{id}
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new ApiResponse(404));
        }

        [HttpPost]  //POST: /api/users
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            var result = await _userService.CreateUserAsync(user);
            return result.IsSuccess ? CreatedAtAction(nameof(GetUserById), new { id = user.Id }, result.Data) : BadRequest(new ApiResponse(400));
        }

        [HttpPut("{id}")]  //PUT: /api/users/{id}
        public async Task<ActionResult<User>> UpdateUser(Guid id, [FromBody] User user)
        {
            var result = await _userService.UpdateUserAsync(id, user);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new ApiResponse(400));
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return result.IsSuccess;
        }
    }
}
