namespace SportsClubApi.Models
{
    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string Password { get; set; } = null!;
    }
}
