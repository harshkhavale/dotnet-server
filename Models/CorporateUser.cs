using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class CorporateUser
{
    public int CorporateId { get; set; }

    public string CorporateName { get; set; } = null!;

    public string ContactPersonName { get; set; } = null!;

    public string? ContactPersonDetail { get; set; }

    public string ContactPersonMobileNo { get; set; } = null!;

    public string? ContactPersonEmail { get; set; }

    public string? EmailIdentifier { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime ModifiedDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<MembershipPlan> MembershipPlans { get; set; } = new List<MembershipPlan>();
}
