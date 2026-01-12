using BackEnd.DTOs;
using BackEnd.Models;
using BackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services
{
    public class UserService : IUserService
    {
        private readonly UserManagementDbContext _context;

        public UserService(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateUserAsync(UserRequestDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Code == request.Code))
            {
                throw new Exception("Exist Code");
            }
            if (!string.IsNullOrEmpty(request.Email) && await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                throw new Exception("Email has used");
            }

            var newUser = new User
            {
                Code = request.Code,
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<bool> UpdateUserAsync(int id, UserRequestDto request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            if (user.Code != request.Code && await _context.Users.AnyAsync(u => u.Code == request.Code))
            {
                throw new Exception("Exist Code , try new code");
            }
            if (!string.IsNullOrEmpty(request.Email) && await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                throw new Exception("Email has used");
            }
            user.Code = request.Code;
            user.FullName = request.FullName;
            user.DateOfBirth = request.DateOfBirth;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Address = request.Address;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
