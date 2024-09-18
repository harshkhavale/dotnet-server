﻿using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class UserRolesDetail
{
    public int UserRoleId { get; set; }

    public int? UserId { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual User? User { get; set; }
}