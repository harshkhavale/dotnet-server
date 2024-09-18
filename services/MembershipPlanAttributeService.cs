using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsClubApi.Services
{
    public interface IMembershipPlanAttributeService
    {
        Task<IEnumerable<MembershipPlanAttribute>> GetAllAsync();
        Task<MembershipPlanAttribute?> GetByIdAsync(int id);
        Task<MembershipPlanAttribute> CreateAsync(MembershipPlanAttribute attribute);
        Task<MembershipPlanAttribute?> UpdateAsync(MembershipPlanAttribute attribute);
        Task<bool> DeleteAsync(int id);
    }
    public class MembershipPlanAttributeService : IMembershipPlanAttributeService
    {
        private readonly SportsClubContext _context;

        public MembershipPlanAttributeService(SportsClubContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MembershipPlanAttribute>> GetAllAsync()
        {
            return await _context.MembershipPlanAttribute.ToListAsync();
        }

        public async Task<MembershipPlanAttribute?> GetByIdAsync(int id)
        {
            return await _context.MembershipPlanAttribute
                .Include(a => a.Plan)
                .FirstOrDefaultAsync(a => a.AttributeId == id);
        }

        public async Task<MembershipPlanAttribute> CreateAsync(MembershipPlanAttribute attribute)
        {
            _context.MembershipPlanAttribute.Add(attribute);
            await _context.SaveChangesAsync();
            return attribute;
        }

        public async Task<MembershipPlanAttribute?> UpdateAsync(MembershipPlanAttribute attribute)
        {
            var existingAttribute = await _context.MembershipPlanAttribute.FindAsync(attribute.AttributeId);
            if (existingAttribute == null) return null;

            existingAttribute.Attributename = attribute.Attributename;
            existingAttribute.Attributedetails = attribute.Attributedetails;

            await _context.SaveChangesAsync();
            return existingAttribute;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attribute = await _context.MembershipPlanAttribute.FindAsync(id);
            if (attribute == null) return false;

            _context.MembershipPlanAttribute.Remove(attribute);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
