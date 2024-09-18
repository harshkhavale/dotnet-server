namespace SportsClubApi.Models;
public class RegisterModel
{
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string Password { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Postalcode { get; set; }
    public int? PlanId { get; set; }
    public int UserCategoryId { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}
