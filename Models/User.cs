using System;
using System.Collections.Generic;

namespace SportsClubApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public string? City { get; set; }

    public string? Postalcode { get; set; }

    public string? Mobile { get; set; }

    public string? GoogleLocation { get; set; }

    public byte[]? ProfilePic { get; set; }

    public string? AboutMe { get; set; }

    public bool? MobileNotification { get; set; }

    public bool? EmailNotification { get; set; }

    public string? Password { get; set; }

    public int? ActivationLevel { get; set; }

    public string? Country { get; set; }

    public int? CountryId { get; set; }

    public decimal? CreatedBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public decimal? LastModifiedBy { get; set; }

    public DateTime? LastModifiedDateTime { get; set; }

    public int? IsDeleted { get; set; }

    public int? EmailVerified { get; set; }

    public int? MobileVerified { get; set; }

    public bool? IsVerified { get; set; }

    public string? State { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    public int? Age { get; set; }

    public int? UserCategoryId { get; set; }

    public bool? PasswordReset { get; set; }

    public bool? DataVerified { get; set; }

    public DateTime? DataVerificationDate { get; set; }

    public string? Email { get; set; }

    public string? EmailVerificationCode { get; set; }

    public string? MobileVerificationCode { get; set; }

    public virtual ICollection<UserConsentDetail> UserConsentDetails { get; set; } = new List<UserConsentDetail>();

    public virtual ICollection<UserMembershipPlanDetail> UserMembershipPlanDetails { get; set; } = new List<UserMembershipPlanDetail>();

    public virtual ICollection<UserNotificationHistory> UserNotificationHistories { get; set; } = new List<UserNotificationHistory>();

    public virtual ICollection<UserPaymentDetail> UserPaymentDetails { get; set; } = new List<UserPaymentDetail>();

    public virtual ICollection<UserRolesDetail> UserRolesDetails { get; set; } = new List<UserRolesDetail>();
        public virtual UserCategory? UserCategory { get; set; } // Navigation property

}
