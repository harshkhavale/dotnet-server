public class UserDto
{
    public int UserId { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string? PostalCode { get; set; } = null!;
    public int? EmailVerified { get; set; }
    public int? MobileVerified { get; set; }
    public string? UserType { get; set; }
    public int? PlanId { get; set; }  // PlanId from MembershipPlan
    public string? PlanName { get; set; }  // PlanName from MembershipPlan
     public string? GoogleLocation { get; set; }

    public byte[]? ProfilePic { get; set; }

    public string? AboutMe { get; set; }

    public bool? MobileNotification { get; set; }

    public bool? EmailNotification { get; set; }

    public string? Password { get; set; }

    public int? ActivationLevel { get; set; }

    public string? Country { get; set; }

    public int? CountryId { get; set; }
}
