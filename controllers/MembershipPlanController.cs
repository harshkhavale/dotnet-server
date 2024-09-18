using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsClubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipPlanController : ControllerBase
    {
        private readonly SportsClubContext _context;

        public MembershipPlanController(SportsClubContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "All")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipPlanDto>>> GetMembershipPlans()
        {
            var membershipPlans = await _context.MembershipPlans
                .Where(mp => mp.CorporateId == null)
                .Include(mp => mp.MembershipPlanAttributes)
                .Select(mp => new MembershipPlanDto
                {
                    PlanId = mp.PlanId,
                    PlanName = mp.PlanName,
                    Description = mp.Description,
                    Price = mp.Price,
                    CreatedDateTime = mp.CreatedDateTime,
                    CreatedBy = mp.CreatedBy,
                    ModifiedDateTime = mp.ModifiedDateTime,
                    MembershipPlanAttributes = mp.MembershipPlanAttributes.Select(attr => new MembershipPlanAttributeDto
                    {
                        AttributeId = attr.AttributeId,
                        AttributeName = attr.Attributename,
                        AttributeDetails = attr.Attributedetails
                    }).ToList()
                })
                .ToListAsync();

            return Ok(membershipPlans);
        }


        [Authorize(Policy = "All")]
        [HttpGet("allplans")]
        public async Task<ActionResult<IEnumerable<MembershipPlanDto>>> GetAllMembershipPlans()
        {
            var membershipPlans = await _context.MembershipPlans
                .Include(mp => mp.MembershipPlanAttributes)
                .Select(mp => new MembershipPlanDto
                {
                    PlanId = mp.PlanId,
                    PlanName = mp.PlanName,
                    Description = mp.Description,
                    Price = mp.Price,
                    CreatedDateTime = mp.CreatedDateTime,
                    CreatedBy = mp.CreatedBy,
                    ModifiedDateTime = mp.ModifiedDateTime,
                    CorporateId = mp.CorporateId,
                    MembershipPlanAttributes = mp.MembershipPlanAttributes.Select(attr => new MembershipPlanAttributeDto
                    {
                        AttributeId = attr.AttributeId,
                        AttributeName = attr.Attributename,
                        AttributeDetails = attr.Attributedetails
                    }).ToList()
                })
                .ToListAsync();

            return Ok(membershipPlans);
        }


        [Authorize(Policy = "All")]
        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipPlanDto>> GetMembershipPlan(int id)
        {
            var membershipPlan = await _context.MembershipPlans
                .Include(mp => mp.MembershipPlanAttributes)
                .Where(mp => mp.PlanId == id)
                .Select(mp => new MembershipPlanDto
                {
                    PlanId = mp.PlanId,
                    PlanName = mp.PlanName,
                    Description = mp.Description,
                    Price = mp.Price,
                    CreatedDateTime = mp.CreatedDateTime,
                    CreatedBy = mp.CreatedBy,
                    ModifiedDateTime = mp.ModifiedDateTime,
                    MembershipPlanAttributes = mp.MembershipPlanAttributes.Select(attr => new MembershipPlanAttributeDto
                    {
                        AttributeId = attr.AttributeId,
                        AttributeName = attr.Attributename,
                        AttributeDetails = attr.Attributedetails
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (membershipPlan == null)
            {
                return NotFound();
            }

            return Ok(membershipPlan);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMembershipPlan(int id, MembershipPlan membershipPlan)
        {
            // Retrieve the existing membership plan from the database
            var existingPlan = await _context.MembershipPlans.FindAsync(id);
            if (existingPlan == null)
            {
                return NotFound();
            }

            // Update only the fields that are provided in the request
            if (!string.IsNullOrEmpty(membershipPlan.PlanName))
            {
                existingPlan.PlanName = membershipPlan.PlanName;
            }

            if (!string.IsNullOrEmpty(membershipPlan.Description))
            {
                existingPlan.Description = membershipPlan.Description;
            }

            if (membershipPlan.Price > 0)
            {
                existingPlan.Price = membershipPlan.Price;
            }

            if (membershipPlan.CreatedDateTime != default(DateTime))
            {
                existingPlan.CreatedDateTime = membershipPlan.CreatedDateTime;
            }

            if (membershipPlan.ModifiedDateTime != default(DateTime))
            {
                existingPlan.ModifiedDateTime = membershipPlan.ModifiedDateTime;
            }

            if (!string.IsNullOrEmpty(membershipPlan.CreatedBy))
            {
                existingPlan.CreatedBy = membershipPlan.CreatedBy;
            }

            if (!string.IsNullOrEmpty(membershipPlan.ModifiedBy))
            {
                existingPlan.ModifiedBy = membershipPlan.ModifiedBy;
            }

            // Mark the entity as modified and save changes
            _context.Entry(existingPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembershipPlanExists(id))
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
        [HttpPost]
        public async Task<ActionResult<MembershipPlan>> PostMembershipPlan(MembershipPlan membershipPlan)
        {
            _context.MembershipPlans.Add(membershipPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMembershipPlan", new { id = membershipPlan.PlanId }, membershipPlan);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembershipPlan(int id)
        {
            var membershipPlan = await _context.MembershipPlans.FindAsync(id);
            if (membershipPlan == null)
            {
                return NotFound();
            }

            _context.MembershipPlans.Remove(membershipPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [Authorize(Policy = "All")]

        [HttpPost("getcorporateplans")]
        public async Task<ActionResult<IEnumerable<MembershipPlanDto>>> GetCorporatePlans([FromBody] CorporateIdRequest request)
        {
            List<MembershipPlanDto> membershipPlans;

            if (request.CorporateId == "*")
            {
                membershipPlans = await _context.MembershipPlans
                    .Where(mp => mp.CorporateId != null)
                    .Include(mp => mp.MembershipPlanAttributes)
                    .Select(mp => new MembershipPlanDto
                    {
                        PlanId = mp.PlanId,
                        PlanName = mp.PlanName,
                        Description = mp.Description,
                        Price = mp.Price,
                        CreatedDateTime = mp.CreatedDateTime,
                        CreatedBy = mp.CreatedBy,
                        ModifiedDateTime = mp.ModifiedDateTime,
                        CorporateId = mp.CorporateId,
                        MembershipPlanAttributes = mp.MembershipPlanAttributes.Select(attr => new MembershipPlanAttributeDto
                        {
                            AttributeId = attr.AttributeId,
                            AttributeName = attr.Attributename,
                            AttributeDetails = attr.Attributedetails
                        }).ToList()
                    })
                    .ToListAsync();
            }
            else
            {
                // Assume CorporateId is an integer and fetch plans for that CorporateId
                var corporateId = int.Parse(request.CorporateId); // Parse the string to an integer
                membershipPlans = await _context.MembershipPlans
                    .Where(mp => mp.CorporateId == corporateId)
                    .Include(mp => mp.MembershipPlanAttributes)
                    .Select(mp => new MembershipPlanDto
                    {
                        PlanId = mp.PlanId,
                        PlanName = mp.PlanName,
                        Description = mp.Description,
                        Price = mp.Price,
                        CreatedDateTime = mp.CreatedDateTime,
                        CreatedBy = mp.CreatedBy,
                        ModifiedDateTime = mp.ModifiedDateTime,
                        CorporateId = mp.CorporateId,
                        MembershipPlanAttributes = mp.MembershipPlanAttributes.Select(attr => new MembershipPlanAttributeDto
                        {
                            AttributeId = attr.AttributeId,
                            AttributeName = attr.Attributename,
                            AttributeDetails = attr.Attributedetails
                        }).ToList()
                    })
                    .ToListAsync();
            }

            if (membershipPlans == null || !membershipPlans.Any())
            {
                return NotFound("No membership plans found for the given corporate ID.");
            }

            return Ok(membershipPlans);
        }



        private bool MembershipPlanExists(int id)
        {
            return _context.MembershipPlans.Any(e => e.PlanId == id);
        }
    }

    public class CorporateIdRequest
    {
        public string CorporateId { get; set; }
    }


}
