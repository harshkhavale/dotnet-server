using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class UserMembershipPlanDetail
{
    public int UserPlanId { get; set; }

    public int? UserId { get; set; }

    public int? PlanId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual MembershipPlan? Plan { get; set; }

    public virtual User? User { get; set; }
}
