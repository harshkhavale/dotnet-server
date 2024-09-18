using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsClubApi.Models;
using SportsClubApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsClubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipPlanAttributeController : ControllerBase
    {
        private readonly IMembershipPlanAttributeService _attributeService;

        public MembershipPlanAttributeController(IMembershipPlanAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [Authorize(Policy = "All")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipPlanAttribute>>> GetAll()
        {
            var attributes = await _attributeService.GetAllAsync();
            return Ok(attributes);
        }

        [Authorize(Policy = "All")]
        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipPlanAttribute>> GetById(int id)
        {
            var attribute = await _attributeService.GetByIdAsync(id);
            if (attribute == null)
            {
                return NotFound();
            }
            return Ok(attribute);
        }
        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost]
        public async Task<ActionResult<MembershipPlanAttribute>> Create(MembershipPlanAttribute attribute)
        {
            var createdAttribute = await _attributeService.CreateAsync(attribute);
            return CreatedAtAction(nameof(GetById), new { id = createdAttribute.AttributeId }, createdAttribute);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MembershipPlanAttribute>> Update(int id, MembershipPlanAttribute attribute)
        {
            if (id != attribute.AttributeId)
            {
                return BadRequest();
            }

            var updatedAttribute = await _attributeService.UpdateAsync(attribute);
            if (updatedAttribute == null)
            {
                return NotFound();
            }

            return Ok(updatedAttribute);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _attributeService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
