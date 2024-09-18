using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class Cities
{
    public decimal CityId { get; set; }

    public decimal? CountryId { get; set; }

    public string CityName { get; set; } = null!;

    public decimal? StateId { get; set; }
}
