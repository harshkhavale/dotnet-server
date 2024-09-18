using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserMembershipPlanDetailService
{
    Task<IEnumerable<UserMembershipPlanDetail>> GetAllUserMembershipPlanDetailsAsync();
    Task<UserMembershipPlanDetail?> GetUserMembershipPlanDetailByIdAsync(int id);
    Task<UserMembershipPlanDetail> CreateUserMembershipPlanDetailAsync(UserMembershipPlanDetail userPlanDetail);
    Task<UserMembershipPlanDetail?> UpdateUserMembershipPlanDetailByUserAndPlanAsync(UserMembershipPlanDetail userPlanDetail);
    Task<bool> DeleteUserMembershipPlanDetailAsync(int id);
    Task<MembershipPlan?> GetUserPlanByUserIdAsync(int userId);

}
public class UserMembershipPlanDetailService : IUserMembershipPlanDetailService
{
    private readonly SportsClubContext _context;

    public UserMembershipPlanDetailService(SportsClubContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserMembershipPlanDetail>> GetAllUserMembershipPlanDetailsAsync()
    {
        return await _context.UserMembershipPlanDetails
            .ToListAsync();
    }

    public async Task<UserMembershipPlanDetail?> GetUserMembershipPlanDetailByIdAsync(int id)
    {
        return await _context.UserMembershipPlanDetails
            .SingleOrDefaultAsync(u => u.UserPlanId == id);
    }

    public async Task<UserMembershipPlanDetail> CreateUserMembershipPlanDetailAsync(UserMembershipPlanDetail userPlanDetail)
    {
        _context.UserMembershipPlanDetails.Add(userPlanDetail);
        await _context.SaveChangesAsync();
        return userPlanDetail;
    }
   public async Task<UserMembershipPlanDetail?> UpdateUserMembershipPlanDetailByUserAndPlanAsync(UserMembershipPlanDetail userPlanDetail)
{
    var existingUserPlan = await _context.UserMembershipPlanDetails
        .SingleOrDefaultAsync(u => u.UserId == userPlanDetail.UserId && u.PlanId == userPlanDetail.PlanId);

    if (existingUserPlan == null) return null;

    // Update the fields
    existingUserPlan.StartDate = userPlanDetail.StartDate;
    existingUserPlan.EndDate = userPlanDetail.EndDate;

    // Explicitly mark the entity as modified
    _context.Entry(existingUserPlan).State = EntityState.Modified;

    // Save the changes
    await _context.SaveChangesAsync();

    return existingUserPlan;
}


 public async Task<MembershipPlan?> GetUserPlanByUserIdAsync(int userId)
{
    var userPlanDetail = await _context.UserMembershipPlanDetails
        .Include(up => up.Plan)
        .SingleOrDefaultAsync(up => up.UserId == userId);

    if (userPlanDetail == null)
    {
        // Log or debug here
        Console.WriteLine($"No user membership plan details found for userId: {userId}");
    }
    
    return userPlanDetail?.Plan;
}


    public async Task<bool> DeleteUserMembershipPlanDetailAsync(int id)
    {
        var userPlan = await _context.UserMembershipPlanDetails.FindAsync(id);
        if (userPlan == null) return false;

        _context.UserMembershipPlanDetails.Remove(userPlan);
        await _context.SaveChangesAsync();
        return true;
    }
}
