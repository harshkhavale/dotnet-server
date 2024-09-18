using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class UserCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;
    public virtual ICollection<User> Users { get; set; } = new List<User>();

}
