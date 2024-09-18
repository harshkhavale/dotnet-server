using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class MembershipPlanAttribute
{
    public int AttributeId { get; set; }

    public int? PlanId { get; set; }

    public string Attributename { get; set; } = null!;

    public string? Attributedetails { get; set; }

    public virtual MembershipPlan? Plan { get; set; }
}
