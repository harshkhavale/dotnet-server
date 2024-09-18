using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SportsClubApi.Models;
using SportsClubApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsClubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CorporateUserController : ControllerBase
    {
        private readonly ICorporateUserService _corporateUserService;

        public CorporateUserController(ICorporateUserService corporateUserService)
        {
            _corporateUserService = corporateUserService;
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CorporateUser>>> GetAll()
        {
            var corporateUsers = await _corporateUserService.GetAllAsync();
            return Ok(corporateUsers);
        }
        [Authorize(Policy = "RequireAdministratorRole")]

        [HttpGet("{id}")]
        public async Task<ActionResult<CorporateUser>> GetById(int id)
        {
            var corporateUser = await _corporateUserService.GetByIdAsync(id);
            if (corporateUser == null)
            {
                return NotFound();
            }
            return Ok(corporateUser);
        }
        [Authorize(Policy = "RequireAdministratorRole")]

        [HttpPost]
        public async Task<ActionResult<CorporateUser>> Create(CorporateUser corporateUser)
        {
            var createdCorporateUser = await _corporateUserService.CreateAsync(corporateUser);
            return CreatedAtAction(nameof(GetById), new { id = createdCorporateUser.CorporateId }, createdCorporateUser);
        }
        private bool IsValidSqlDateTime(DateTime dateTime)
        {
            return dateTime >= (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue &&
                   dateTime <= (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPut("{id}")]
        public async Task<ActionResult<CorporateUser>> UpdateAsync(int id, CorporateUser corporateUser)
        {
            if (id != corporateUser.CorporateId)
            {
                return BadRequest();
            }

            if (!IsValidSqlDateTime(corporateUser.CreatedDateTime))
            {
                corporateUser.CreatedDateTime = DateTime.UtcNow;
            }

            var updatedCorporateUser = await _corporateUserService.UpdateAsync(corporateUser);
            return Ok(updatedCorporateUser);
        }



        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _corporateUserService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Policy = "RequireUnauthorizedRole")]
        [HttpPost("verifycorporateuser")]
        public async Task<ActionResult<int?>> VerifyCorporateUser([FromBody] EmailRequest2 request)
        {
            var emailDomain = request.Email.Split('@').LastOrDefault();

            if (string.IsNullOrEmpty(emailDomain))
            {
                return BadRequest("Invalid email format.");
            }

            var corporateUser = await _corporateUserService.GetByEmailIdentifierAsync(emailDomain);

            if (corporateUser == null)
            {
                return NotFound();
            }

            return Ok(corporateUser);
        }
    }

    public class EmailRequest2
    {
        public string Email { get; set; } = null!;
    }
}
