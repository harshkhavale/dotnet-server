using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class MembershipPlan
{
    public int PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime ModifiedDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public int? CorporateId { get; set; }

    public decimal? CorporateShare { get; set; }

    public virtual CorporateUser? Corporate { get; set; }

    public virtual ICollection<MembershipPlanAttribute> MembershipPlanAttributes { get; set; } = new List<MembershipPlanAttribute>();

    public virtual ICollection<UserMembershipPlanDetail> UserMembershipPlanDetails { get; set; } = new List<UserMembershipPlanDetail>();

    public virtual ICollection<UserPaymentDetail> UserPaymentDetails { get; set; } = new List<UserPaymentDetail>();
}
