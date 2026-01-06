using CP.Server.Services;
using CP.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<BusinessDto>>> GetAll()
        {
            return Ok(await _businessService.GetAllAsync());
        }

        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BusinessDto>>> GetActive()
        {
            var businesses = await _businessService.GetAllAsync();
            return Ok(businesses.Where(b => b.IsActive));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BusinessDto>> GetById(Guid id)
        {
            var business = await _businessService.GetByIdAsync(id);
            if (business == null) return NotFound();
            return Ok(business);
        }

        [HttpPost]
        [Authorize(Roles = "Business")]
        public async Task<ActionResult<BusinessDto>> Create([FromForm] CreateBusinessDto createDto, IFormFile? image)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();
            
            // Ensure business is created for the authenticated user
            createDto.UserId = Guid.Parse(userIdClaim.Value);

            Stream? stream = null;
            if (image != null)
            {
                stream = image.OpenReadStream();
            }

            var result = await _businessService.CreateAsync(createDto, stream, image?.FileName);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Business")]
        public async Task<ActionResult<BusinessDto>> Update(Guid id, [FromForm] UpdateBusinessDto updateDto, IFormFile? image)
        {
            if (id != updateDto.Id) return BadRequest();

            var existing = await _businessService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Ownership check for Business role
            if (User.IsInRole("Business"))
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                if (userIdClaim == null || existing.UserId != Guid.Parse(userIdClaim.Value))
                {
                    return Forbid();
                }
            }

            Stream? stream = null;
            if (image != null)
            {
                stream = image.OpenReadStream();
            }

            var result = await _businessService.UpdateAsync(id, updateDto, stream, image?.FileName);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Business")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _businessService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Ownership check for Business role
            if (User.IsInRole("Business"))
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                if (userIdClaim == null || existing.UserId != Guid.Parse(userIdClaim.Value))
                {
                    return Forbid();
                }
            }

            var success = await _businessService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("mine")]
        public async Task<ActionResult<IEnumerable<BusinessDto>>> GetMyBusinesses()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);
            return Ok(await _businessService.GetByUserIdAsync(userId));
        }
    }
}
