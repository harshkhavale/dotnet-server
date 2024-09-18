using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class Activity
{
    public int ActivityId { get; set; }

    public string ActivityName { get; set; } = null!;

    public string? ActivityDescription { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<SupplierActivitiesDetail> SupplierActivitiesDetails { get; set; } = new List<SupplierActivitiesDetail>();
}
