using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;
using SportsClubApi.Services;
using System.Threading.Tasks;

namespace SportsClubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserMembershipPlanDetailService _userMembershipPlanDetailService;

        public AuthController(IAuthService authService, IUserMembershipPlanDetailService userMembershipPlanDetailService)
        {
            _authService = authService;
            _userMembershipPlanDetailService = userMembershipPlanDetailService;
        }

        [Authorize(Policy = "All")]
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) && string.IsNullOrWhiteSpace(model.Mobile))
            {
                return BadRequest(new { message = "Either email or mobile number is required." });
            }

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                return BadRequest(new { message = "First name is required." });
            }

            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                return BadRequest(new { message = "Last name is required." });
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                var existingUserByEmail = await _authService.GetUserByEmailAsync(model.Email);
                if (existingUserByEmail != null)
                {
                    return Conflict(new { message = "A user with this email already exists." });
                }
            }

            if (!string.IsNullOrWhiteSpace(model.Mobile))
            {
                var existingUserByMobile = await _authService.GetUserByMobileAsync(model.Mobile);
                if (existingUserByMobile != null)
                {
                    return Conflict(new { message = "A user with this mobile number already exists." });
                }
            }

            try
            {
                var user = new User
                {
                    Email = model.Email,
                    Mobile = model.Mobile,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Postalcode = model.Postalcode,
                    UserCategoryId = model.UserCategoryId
                };

                var createdUser = await _authService.Register(user, model.Password);

                if (createdUser == null)
                {
                    return BadRequest(new { message = "Registration failed. Please try again." });
                }

                var userMembershipPlanDetail = new UserMembershipPlanDetail
                {
                    UserId = createdUser.UserId,
                    PlanId = model.PlanId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };

                await _userMembershipPlanDetailService.CreateUserMembershipPlanDetailAsync(userMembershipPlanDetail);

                var authDto = await _authService.Authenticate(model.Email, model.Mobile, model.Password);

                if (authDto != null)
                {
                    return Ok(authDto);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Registration successful, but token generation failed." });
                }
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true)
            {
                return Conflict(new { message = "A user with this email or mobile number already exists." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [Authorize(Policy = "All")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) && string.IsNullOrWhiteSpace(model.Mobile))
            {
                return BadRequest(new { message = "Either email or mobile number is required." });
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest(new { message = "Password is required." });
            }

            var authDto = await _authService.Authenticate(model.Email, model.Mobile, model.Password);

            if (authDto == null)
            {
                bool userExists = false;

                if (!string.IsNullOrWhiteSpace(model.Email))
                {
                    var userByEmail = await _authService.GetUserByEmailAsync(model.Email);
                    if (userByEmail != null)
                    {
                        userExists = true;
                    }
                }

                if (!string.IsNullOrWhiteSpace(model.Mobile))
                {
                    var userByMobile = await _authService.GetUserByMobileAsync(model.Mobile);
                    if (userByMobile != null)
                    {
                        userExists = true;
                    }
                }

                if (!userExists)
                {
                    return NotFound(new { message = "No account found with the provided email or mobile number." });
                }
                else
                {
                    return Unauthorized(new { message = "Invalid credentials. Please check your password and try again." });
                }
            }

            return Ok(authDto);
        }

        [HttpGet("unauthorized-token")]
        public IActionResult GetUnauthorizedToken()
        {
            var token = _authService.GetUnauthorizedToken();
            return Ok(new { Token = token });
        }
    }
}
