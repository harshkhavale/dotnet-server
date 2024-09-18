using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsClubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsClubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [Authorize(Policy = "All")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<States>>> GetStates()
        {
            var states = await _stateService.GetAllStatesAsync();
            return Ok(states);
        }

        [Authorize(Policy = "All")]
        [HttpGet("{id}")]
        public async Task<ActionResult<States>> GetState(decimal id)
        {
            var state = await _stateService.GetStateByIdAsync(id);
            if (state == null)
                return NotFound();

            return Ok(state);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost]
        public async Task<ActionResult<States>> PostState(States state)
        {
            var createdState = await _stateService.CreateStateAsync(state);
            return CreatedAtAction(nameof(GetState), new { id = createdState.StateId }, createdState);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutState(decimal id, States state)
        {
            if (id != state.StateId)
                return BadRequest();

            var updatedState = await _stateService.UpdateStateAsync(id, state);
            if (updatedState == null)
                return NotFound();

            return NoContent();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(decimal id)
        {
            var success = await _stateService.DeleteStateAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
