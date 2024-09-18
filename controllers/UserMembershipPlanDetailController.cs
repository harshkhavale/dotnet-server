using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsClubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsClubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserMembershipPlanDetailController : ControllerBase
    {
        private readonly IUserMembershipPlanDetailService _service;

        public UserMembershipPlanDetailController(IUserMembershipPlanDetailService service)
        {
            _service = service;
        }

        [Authorize(Policy = "All")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserMembershipPlanDetail>>> GetUserMembershipPlanDetails()
        {
            var userPlans = await _service.GetAllUserMembershipPlanDetailsAsync();
            return Ok(userPlans);
        }
      [Authorize(Policy = "All")]
[HttpGet("GetUserPlan/{userId}")]
public async Task<ActionResult<MembershipPlan>> GetUserPlan(int userId)
{
    var userPlan = await _service.GetUserPlanByUserIdAsync(userId);

    if (userPlan == null)
        return NotFound();

    return Ok(userPlan);
}


        [Authorize(Policy = "All")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserMembershipPlanDetail>> GetUserMembershipPlanDetail(int id)
        {
            var userPlan = await _service.GetUserMembershipPlanDetailByIdAsync(id);

            if (userPlan == null)
                return NotFound();

            return Ok(userPlan);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost]
        public async Task<ActionResult<UserMembershipPlanDetail>> PostUserMembershipPlanDetail(UserMembershipPlanDetail userPlanDetail)
        {
            var createdUserPlan = await _service.CreateUserMembershipPlanDetailAsync(userPlanDetail);
            return CreatedAtAction(nameof(GetUserMembershipPlanDetail), new { id = createdUserPlan.UserPlanId }, createdUserPlan);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserMembershipPlanDetail([FromBody] UserMembershipPlanDetail userPlanDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPlanDetail = await _service.UpdateUserMembershipPlanDetailByUserAndPlanAsync(userPlanDetail);

            if (updatedPlanDetail == null)
            {
                return NotFound();  // Record not found
            }

            return Ok(updatedPlanDetail);
        }


        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserMembershipPlanDetail(int id)
        {
            var result = await _service.DeleteUserMembershipPlanDetailAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
