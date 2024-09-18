using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? SupplierType { get; set; }

    public string? SupplierAddress { get; set; }

    public string? SupplierContact { get; set; }

    public virtual ICollection<SupplierActivitiesDetail> SupplierActivitiesDetails { get; set; } = new List<SupplierActivitiesDetail>();
}
