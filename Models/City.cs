using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class City
{
    public decimal CityId { get; set; }

    public decimal? CountryId { get; set; }

    public string? CityName { get; set; }

    public decimal? StateId { get; set; }
}
