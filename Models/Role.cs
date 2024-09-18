using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRolesDetail> UserRolesDetails { get; set; } = new List<UserRolesDetail>();
}
