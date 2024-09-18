using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SportsClubApi.Models;

namespace SportsClubApi.Services
{
    public interface IAuthService
    {
        Task<User?> Register(User user, string password);
        Task<AuthDto?> Authenticate(string? email, string? mobile, string password);
        Task<String> GetUnauthorizedToken();
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByMobileAsync(string mobile);
    }
    public class AuthService : IAuthService
    {
        private readonly SportsClubContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(SportsClubContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User?> Register(User user, string password)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<AuthDto?> Authenticate(string? email, string? mobile, string password)
        {
            User? user = null;

            if (!string.IsNullOrEmpty(email))
            {
                user = await _context.Users
                    .Include(u => u.UserCategory)
                    .FirstOrDefaultAsync(u => u.Email == email);
            }
            else if (!string.IsNullOrEmpty(mobile))
            {
                user = await _context.Users
                    .Include(u => u.UserCategory)
                    .FirstOrDefaultAsync(u => u.Mobile == mobile);
            }
            else
            {
                return null;
            }

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.MobilePhone, user.Mobile ?? ""),
            new Claim(ClaimTypes.Role, user.UserCategory?.CategoryName ?? "Normal")
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            UserDto User = new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Mobile = user.Mobile,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PostalCode = user.Postalcode,
                EmailVerified = user.EmailVerified,
                MobileVerified = user.MobileVerified,
                UserType = user.UserCategory?.CategoryName,
                GoogleLocation = user.GoogleLocation,

                ProfilePic = user.ProfilePic,

                AboutMe = user.AboutMe,
                MobileNotification = user.MobileNotification,
                EmailNotification = user.EmailNotification,

                Password = user.Password,
                ActivationLevel = user.ActivationLevel,

                Country = user.Country,
                CountryId = user.CountryId

            };

            return new AuthDto
            {
                Token = tokenString,
                User = User
            };
        }

        public async Task<UserCategory?> GetUserCategoryByIdAsync(int categoryId)
        {
            return await _context.UserCategory
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<string> GetUnauthorizedToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Role, "Unauthorized")  // Set the role to Unauthorized
        }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["Jwt:Audience"], // Add audience claim
                Issuer = _configuration["Jwt:Issuer"]     // Optionally add issuer claim
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);  // Return only the token string
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByMobileAsync(string mobile)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Mobile == mobile);
        }
    }

}
