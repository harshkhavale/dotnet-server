public class UserMembershipPlanDetailDto
{
    public int UserPlanId { get; set; }

    public int? UserId { get; set; }

    public int? PlanId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

}

