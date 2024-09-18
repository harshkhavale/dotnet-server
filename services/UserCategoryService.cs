using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;

namespace SportsClubApi.Services
{
    public class UserCategoryService
    {
        private readonly SportsClubContext _context;

        public UserCategoryService(SportsClubContext context)
        {
            _context = context;
        }

        public async Task<List<UserCategory>> GetAllUserCategoriesAsync()
        {
            return await _context.UserCategory.ToListAsync();
        }

        public async Task<UserCategory> GetUserCategoryByIdAsync(int id)
        {
            return await _context.UserCategory.FindAsync(id);
        }

        public async Task<UserCategory> AddUserCategoryAsync(UserCategory category)
        {
            _context.UserCategory.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<UserCategory> UpdateUserCategoryAsync(UserCategory category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteUserCategoryAsync(int id)
        {
            var category = await _context.UserCategory.FindAsync(id);
            if (category != null)
            {
                _context.UserCategory.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
