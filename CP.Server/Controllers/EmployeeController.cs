using CP.Server.Services;
using CP.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IBusinessService _businessService;

        public EmployeeController(IEmployeeService employeeService, IBusinessService businessService)
        {
            _employeeService = employeeService;
            _businessService = businessService;
        }

        [HttpGet("business/{businessId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetByBusiness(Guid businessId)
        {
            return Ok(await _employeeService.GetByBusinessIdAsync(businessId));
        }

        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetActive()
        {
            return Ok(await _employeeService.GetActiveAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(Guid id)
        {
            var result = await _employeeService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Business")]
        public async Task<ActionResult<EmployeeDto>> Create(CreateEmployeeDto createDto)
        {
            // Business ownership check
            var business = await _businessService.GetByIdAsync(createDto.BusinessId);
            if (business == null) return NotFound("Business not found");

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || business.UserId != Guid.Parse(userIdClaim.Value)) return Forbid();

            var result = await _employeeService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Business")]
        public async Task<ActionResult<EmployeeDto>> Update(Guid id, UpdateEmployeeDto updateDto)
        {
            if (id != updateDto.Id) return BadRequest();

            var existing = await _employeeService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var business = await _businessService.GetByIdAsync(existing.BusinessId);
            if (business == null) return NotFound("Business not found");

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || business.UserId != Guid.Parse(userIdClaim.Value)) return Forbid();

            var result = await _employeeService.UpdateAsync(id, updateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Business")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _employeeService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var business = await _businessService.GetByIdAsync(existing.BusinessId);
            if (business == null) return NotFound("Business not found");

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || business.UserId != Guid.Parse(userIdClaim.Value)) return Forbid();

            await _employeeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
