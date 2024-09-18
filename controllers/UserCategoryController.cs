using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;
using SportsClubApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsClubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCategoryController : ControllerBase
    {
        private readonly UserCategoryService _userCategoryService;

        public UserCategoryController(UserCategoryService userCategoryService)
        {
            _userCategoryService = userCategoryService;
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<UserCategory>>> GetUserCategories()
        {
            return await _userCategoryService.GetAllUserCategoriesAsync();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCategory>> GetUserCategory(int id)
        {
            var category = await _userCategoryService.GetUserCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost]
        public async Task<ActionResult<UserCategory>> PostUserCategory(UserCategory category)
        {
            await _userCategoryService.AddUserCategoryAsync(category);

            return CreatedAtAction(nameof(GetUserCategory), new { id = category.CategoryId }, category);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCategory(int id, UserCategory category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            try
            {
                await _userCategoryService.UpdateUserCategoryAsync(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _userCategoryService.GetUserCategoryByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCategory(int id)
        {
            var category = await _userCategoryService.GetUserCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _userCategoryService.DeleteUserCategoryAsync(id);

            return NoContent();
        }
    }
}
