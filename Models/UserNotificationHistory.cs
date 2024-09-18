using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class UserNotificationHistory
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public string? NotificationMessage { get; set; }

    public DateOnly? NotificationDate { get; set; }

    public virtual User? User { get; set; }
}
