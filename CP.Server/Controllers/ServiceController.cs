using CP.Server.Services;
using CP.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly IBusinessService _businessService;

        public ServiceController(IServiceService serviceService, IBusinessService businessService)
        {
            _serviceService = serviceService;
            _businessService = businessService;
        }

        [HttpGet("business/{businessId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetByBusiness(Guid businessId)
        {
            return Ok(await _serviceService.GetByBusinessIdAsync(businessId));
        }

        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetActive()
        {
            return Ok(await _serviceService.GetActiveAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetById(Guid id)
        {
            var result = await _serviceService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Business")]
        public async Task<ActionResult<ServiceDto>> Create(CreateServiceDto createDto)
        {
            var business = await _businessService.GetByIdAsync(createDto.BusinessId);
            if (business == null) return NotFound("Business not found");

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || business.UserId != Guid.Parse(userIdClaim.Value)) return Forbid();

            var result = await _serviceService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Business")]
        public async Task<ActionResult<ServiceDto>> Update(Guid id, UpdateServiceDto updateDto)
        {
            if (id != updateDto.Id) return BadRequest();

            var existing = await _serviceService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var business = await _businessService.GetByIdAsync(existing.BusinessId);
            if (business == null) return NotFound("Business not found");

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || business.UserId != Guid.Parse(userIdClaim.Value)) return Forbid();

            var result = await _serviceService.UpdateAsync(id, updateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Business")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _serviceService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var business = await _businessService.GetByIdAsync(existing.BusinessId);
            if (business == null) return NotFound("Business not found");

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || business.UserId != Guid.Parse(userIdClaim.Value)) return Forbid();

            await _serviceService.DeleteAsync(id);
            return NoContent();
        }
    }
}
