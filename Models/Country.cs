﻿using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public string? Isocode { get; set; }
}
