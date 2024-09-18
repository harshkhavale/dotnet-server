using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class States
{
    public decimal StateId { get; set; }

    public string? StateName { get; set; }

    public int? CountryId { get; set; }
}
