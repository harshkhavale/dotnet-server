using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class UserConsentDetail
{
    public int ConsentId { get; set; }

    public int? UserId { get; set; }

    public string? ConsentType { get; set; }

    public bool? ConsentStatus { get; set; }

    public DateOnly? ConsentDate { get; set; }

    public virtual User? User { get; set; }
}
