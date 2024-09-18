using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class Countries
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public string? Isocode { get; set; }
}
