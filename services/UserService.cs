using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;

namespace SportsClubApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<User> CreateUserAsync(User user);
        Task<UserDto> UpdateUserAsync(int userId, User user);
        Task<bool> DeleteUserAsync(int userId);
    }
    public class UserService : IUserService
    {

        private readonly SportsClubContext _context;

        public UserService(SportsClubContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.UserCategory)  // Include UserCategory for UserType
                .Include(u => u.UserMembershipPlanDetails)  // Include UserMembershipPlanDetails to fetch Plan details
                .ThenInclude(mp => mp.Plan)  // Include MembershipPlan from UserMembershipPlanDetails
                .ToListAsync();

            // Now process the users in memory to safely handle potential null values
            return users.Select(user => new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Mobile = user.Mobile,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PostalCode = user.Postalcode,
                EmailVerified = user.EmailVerified,
                MobileVerified = user.MobileVerified,
                UserType = user.UserCategory?.CategoryName,  // Get UserType from UserCategory
                PlanId = user.UserMembershipPlanDetails.FirstOrDefault()?.Plan?.PlanId,  // Safely access PlanId in memory
                PlanName = user.UserMembershipPlanDetails.FirstOrDefault()?.Plan?.PlanName  // Safely access PlanName in memory
            })
            .ToList();
        }



        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserCategory)  // Include UserCategory for UserType
                .Include(u => u.UserMembershipPlanDetails)  // Include UserMembershipPlanDetails to fetch Plan details
                .ThenInclude(mp => mp.Plan)  // Include MembershipPlan from UserMembershipPlanDetails
                .SingleOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return null;  // If no user is found, return null
            }

            // Map User to UserDto
            return new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Mobile = user.Mobile,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PostalCode = user.Postalcode,
                EmailVerified = user.EmailVerified,
                MobileVerified = user.MobileVerified,
                UserType = user.UserCategory?.CategoryName,  // Get UserType from UserCategory
                PlanId = user.UserMembershipPlanDetails.FirstOrDefault()?.Plan?.PlanId,  // Safely access PlanId in memory
                PlanName = user.UserMembershipPlanDetails.FirstOrDefault()?.Plan?.PlanName  // Safely access PlanName in memory
            };
        }


        public async Task<User> CreateUserAsync(User user)
        {
            // Set CreatedDateTime and LastModifiedDateTime to current DateTime for a new user
            user.CreatedDateTime = DateTime.Now;
            user.LastModifiedDateTime = DateTime.Now;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserDto?> UpdateUserAsync(int userId, User user)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                return null;  // Return null if the user doesn't exist
            }

            // Update only the properties that are provided in the request
            if (!string.IsNullOrEmpty(user.FirstName)) existingUser.FirstName = user.FirstName;
            if (!string.IsNullOrEmpty(user.LastName)) existingUser.LastName = user.LastName;
            if (!string.IsNullOrEmpty(user.Gender)) existingUser.Gender = user.Gender;
            if (user.Dob != default) existingUser.Dob = user.Dob;
            if (!string.IsNullOrEmpty(user.City)) existingUser.City = user.City;
            if (!string.IsNullOrEmpty(user.Postalcode)) existingUser.Postalcode = user.Postalcode;
            if (!string.IsNullOrEmpty(user.Mobile)) existingUser.Mobile = user.Mobile;
            if (!string.IsNullOrEmpty(user.GoogleLocation)) existingUser.GoogleLocation = user.GoogleLocation;
            if (!string.IsNullOrEmpty(user.AboutMe)) existingUser.AboutMe = user.AboutMe;
            if (user.MobileNotification != null) existingUser.MobileNotification = user.MobileNotification;
            if (user.EmailNotification != null) existingUser.EmailNotification = user.EmailNotification;
            if (!string.IsNullOrEmpty(user.Password)) existingUser.Password = user.Password;
            if (user.ActivationLevel != null) existingUser.ActivationLevel = user.ActivationLevel;
            if (!string.IsNullOrEmpty(user.Country)) existingUser.Country = user.Country;
            if (user.CountryId != null) existingUser.CountryId = user.CountryId;
            if (user.IsDeleted != null) existingUser.IsDeleted = user.IsDeleted;
            if (user.EmailVerified != null) existingUser.EmailVerified = user.EmailVerified;
            if (user.MobileVerified != null) existingUser.MobileVerified = user.MobileVerified;
            if (user.IsVerified != null) existingUser.IsVerified = user.IsVerified;
            if (!string.IsNullOrEmpty(user.State)) existingUser.State = user.State;
            if (user.StateId != null) existingUser.StateId = user.StateId;
            if (user.CityId != null) existingUser.CityId = user.CityId;
            if (user.Age != null) existingUser.Age = user.Age;
            if (user.UserCategoryId != null) existingUser.UserCategoryId = user.UserCategoryId;
            if (user.PasswordReset != null) existingUser.PasswordReset = user.PasswordReset;
            if (user.DataVerified != null) existingUser.DataVerified = user.DataVerified;
            if (user.DataVerificationDate != default) existingUser.DataVerificationDate = user.DataVerificationDate;
            if (!string.IsNullOrEmpty(user.Email)) existingUser.Email = user.Email;
            if (!string.IsNullOrEmpty(user.EmailVerificationCode)) existingUser.EmailVerificationCode = user.EmailVerificationCode;
            if (!string.IsNullOrEmpty(user.MobileVerificationCode)) existingUser.MobileVerificationCode = user.MobileVerificationCode;

            // Always update LastModifiedDateTime to the current time
            existingUser.LastModifiedDateTime = DateTime.Now;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(existingUser.Email))
            {
                user = await _context.Users
                    .Include(u => u.UserCategory)
                    .FirstOrDefaultAsync(u => u.Email == existingUser.Email);
            }
            else if (!string.IsNullOrEmpty(existingUser.Mobile))
            {
                user = await _context.Users
                    .Include(u => u.UserCategory)
                    .FirstOrDefaultAsync(u => u.Mobile == existingUser.Mobile);
            }
            else
            {
                return null;
            }
            UserDto updatedUser = new UserDto
            {
                UserId = existingUser.UserId,
                Email = existingUser.Email,
                Mobile = existingUser.Mobile,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                PostalCode = existingUser.Postalcode,
                EmailVerified = existingUser.EmailVerified,
                MobileVerified = existingUser.MobileVerified,
                UserType = existingUser.UserCategory?.CategoryName,
                 GoogleLocation = existingUser.GoogleLocation,

                ProfilePic = existingUser.ProfilePic,

                AboutMe = existingUser.AboutMe,
                MobileNotification = existingUser.MobileNotification,
                EmailNotification = existingUser.EmailNotification,

                Password = existingUser.Password,
                ActivationLevel = existingUser.ActivationLevel,

                Country = existingUser.Country,
                CountryId = existingUser.CountryId
            };

            return updatedUser;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
