using Microsoft.EntityFrameworkCore;
using QwiikTechnicalTest.Models;
using QwiikTechnicalTest.Models.DB;
using QwiikTechnicalTest.Models.DTO.User;
using System.Collections.Generic;

namespace QwiikTechnicalTest.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserLogin(User dto)
        {
            await _context.Users.AddAsync(dto);
            var isAffected = await _context.SaveChangesAsync();
            return isAffected > 0;
        }

        public async Task<User?> GetUserAdmin(string email)
        {
           return await _context.Users
                .Where(x => x.Email == email && x.Role == UserRole.Admin)
                .FirstOrDefaultAsync();
        }
    }
}
