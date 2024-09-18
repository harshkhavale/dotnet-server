using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsClubApi.Data;
using SportsClubApi.Models;

namespace SportsClubApi.Services
{
    public interface ICorporateUserService
    {
        Task<IEnumerable<CorporateUser>> GetAllAsync();
        Task<CorporateUser> GetByIdAsync(int id);
        Task<CorporateUser> CreateAsync(CorporateUser corporateUser);
        Task<CorporateUser> UpdateAsync(CorporateUser corporateUser);
        Task<bool> DeleteAsync(int id);
        Task<CorporateUser?> GetByEmailIdentifierAsync(string emailIdentifier);
    }
    public class CorporateUserService : ICorporateUserService
    {
        private readonly SportsClubContext _context;

        public CorporateUserService(SportsClubContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CorporateUser>> GetAllAsync()
        {
            return await _context.CorporateUsers.ToListAsync();
        }

        public async Task<CorporateUser> GetByIdAsync(int id)
        {
            return await _context.CorporateUsers.FindAsync(id);
        }

        public async Task<CorporateUser> CreateAsync(CorporateUser corporateUser)
        {
            corporateUser.CreatedDateTime = DateTime.UtcNow;
            corporateUser.ModifiedDateTime = DateTime.UtcNow;
            _context.CorporateUsers.Add(corporateUser);
            await _context.SaveChangesAsync();
            return corporateUser;
        }


        public async Task<CorporateUser> UpdateAsync(CorporateUser corporateUser)
        {
            corporateUser.ModifiedDateTime = DateTime.UtcNow;
            _context.Entry(corporateUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return corporateUser;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var corporateUser = await _context.CorporateUsers.FindAsync(id);
            if (corporateUser == null)
            {
                return false;
            }
            _context.CorporateUsers.Remove(corporateUser);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<CorporateUser?> GetByEmailIdentifierAsync(string emailIdentifier)
        {
            return await _context.CorporateUsers
                                 .FirstOrDefaultAsync(cu => cu.EmailIdentifier == emailIdentifier);
        }


    }
}
