using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class UserPaymentDetail
{
    public int PaymentId { get; set; }

    public int? UserId { get; set; }

    public int? PlanId { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public decimal? Amount { get; set; }

    public virtual MembershipPlan? Plan { get; set; }

    public virtual User? User { get; set; }
}
