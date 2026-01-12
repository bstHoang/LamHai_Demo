using BackEnd.DTOs;
using BackEnd.Models;

namespace BackEnd.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(UserRequestDto request);
        Task<bool> UpdateUserAsync(int id, UserRequestDto request);
        Task<bool> DeleteUserAsync(int id);
    }
}
