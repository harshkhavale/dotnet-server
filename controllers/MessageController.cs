using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsClubApi.Models;
using SportsClubApi.Services;
using SportsClubApi.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportsClubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;
        private readonly ILogger<MessageController> _logger;
        private readonly SportsClubContext _context;

        public MessageController(MessageService emailService, ILogger<MessageController> logger, SportsClubContext context)
        {
            _messageService = emailService;
            _logger = logger;
            _context = context;
        }

        [Authorize(Policy = "All")]
        [HttpPost("sendEmailOtp")]
        public async Task<IActionResult> SendEmailOtp([FromBody] EmailRequest request)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                var otp = VerificationCodeGenerator.GenerateCode();
                await _messageService.SendEmailAsync(request.Email, "Your OTP", $"Your OTP is {otp}");

                user.EmailVerificationCode = otp;
                await _context.SaveChangesAsync();

                return Ok(new { message = "OTP sent to email successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email OTP.");
                return StatusCode(500, new { message = "Internal server error." });
            }
        }

        [Authorize(Policy = "All")]
        [HttpPost("verifyEmailOtp")]
        public async Task<IActionResult> VerifyEmailOtp([FromBody] EmailVerificationRequest request)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email && u.EmailVerified == null);
                if (user == null)
                {
                    return NotFound(new { message = "User not found or already verified." });
                }

                if (user.EmailVerificationCode != request.VerificationCode)
                {
                    return BadRequest(new { message = "Invalid or expired verification code." });
                }

                user.EmailVerified = 1;
                user.EmailVerificationCode = null;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Email verified successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying email OTP.");
                return StatusCode(500, new { message = "Internal server error." });
            }
        }

        [Authorize(Policy = "All")]
        [HttpPost("sendMobileOtp")]
        public async Task<IActionResult> SendMobileOtp([FromBody] MobileRequest request)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Mobile == request.Mobile);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                var otp = VerificationCodeGenerator.GenerateCode();
                await _messageService.SendMobileOTP(request.Mobile, otp);

                user.MobileVerificationCode = otp;
                await _context.SaveChangesAsync();

                return Ok(new { message = "OTP sent to mobile successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending mobile OTP.");
                return StatusCode(500, new { message = "Internal server error." });
            }
        }

        [Authorize(Policy = "All")]
        [HttpPost("verifyMobileOtp")]
        public async Task<IActionResult> VerifyMobileOtp([FromBody] MobileVerificationRequest request)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Mobile == request.Mobile && u.MobileVerified == null);
                if (user == null)
                {
                    return NotFound(new { message = "User not found or already verified." });
                }

                if (user.MobileVerificationCode != request.VerificationCode)
                {
                    return BadRequest(new { message = "Invalid or expired verification code." });
                }

                user.MobileVerified = 1;
                user.MobileVerificationCode = null;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Mobile number verified successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying mobile OTP.");
                return StatusCode(500, new { message = "Internal server error." });
            }
        }
    }

    public class EmailVerificationRequest
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }

    public class EmailRequest
    {
        public string Email { get; set; }
    }

    public class MobileVerificationRequest
    {
        public string Mobile { get; set; }
        public string VerificationCode { get; set; }
    }

    public class MobileRequest
    {
        public string Mobile { get; set; }
    }
}
