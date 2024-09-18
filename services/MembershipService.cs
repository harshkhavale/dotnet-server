using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IMembershipPlanService
{
    Task<IEnumerable<MembershipPlan>> GetAllMembershipPlansAsync();
    Task<MembershipPlan?> GetMembershipPlanByIdAsync(int id);
    Task<MembershipPlan> CreateMembershipPlanAsync(MembershipPlan membershipPlan);
    Task<MembershipPlan?> UpdateMembershipPlanAsync(int id, MembershipPlan membershipPlan);
    Task<bool> DeleteMembershipPlanAsync(int id); 
}
public class MembershipPlanService : IMembershipPlanService
{
    private readonly SportsClubContext _context;

    public MembershipPlanService(SportsClubContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MembershipPlan>> GetAllMembershipPlansAsync()
    {
        return await _context.MembershipPlans
            .ToListAsync();
    }
    

    public async Task<MembershipPlan?> GetMembershipPlanByIdAsync(int id) 
    {
        return await _context.MembershipPlans
            .SingleOrDefaultAsync(mp => mp.PlanId == id);
    }

    public async Task<MembershipPlan> CreateMembershipPlanAsync(MembershipPlan membershipPlan)
    {
        _context.MembershipPlans.Add(membershipPlan);
        await _context.SaveChangesAsync();
        return membershipPlan;
    }

    public async Task<MembershipPlan?> UpdateMembershipPlanAsync(int id, MembershipPlan membershipPlan)
    {
        var existingMembershipPlan = await _context.MembershipPlans.FindAsync(id);
        if (existingMembershipPlan == null) return null;

        existingMembershipPlan.PlanName = membershipPlan.PlanName;
        existingMembershipPlan.Description = membershipPlan.Description;
        existingMembershipPlan.Price = membershipPlan.Price;
        existingMembershipPlan.ModifiedDateTime = membershipPlan.ModifiedDateTime;
        existingMembershipPlan.ModifiedBy = membershipPlan.ModifiedBy;

        await _context.SaveChangesAsync();
        return existingMembershipPlan;
    }

    public async Task<bool> DeleteMembershipPlanAsync(int id)
    {
        var membershipPlan = await _context.MembershipPlans.FindAsync(id);
        if (membershipPlan == null) return false;

        _context.MembershipPlans.Remove(membershipPlan);
        await _context.SaveChangesAsync();
        return true;
    }
}
