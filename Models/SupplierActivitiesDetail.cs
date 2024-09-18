using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class SupplierActivitiesDetail
{
    public int SupplierActivityId { get; set; }

    public int? SupplierId { get; set; }

    public int? ActivityId { get; set; }

    public string? ActivitySchedule { get; set; }

    public virtual Activity? Activity { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
