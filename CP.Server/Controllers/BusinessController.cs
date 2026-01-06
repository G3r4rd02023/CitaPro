using CP.Server.Services;
using CP.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Require auth for all
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessDto>>> GetAll()
        {
            return Ok(await _businessService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessDto>> GetById(Guid id)
        {
            var business = await _businessService.GetByIdAsync(id);
            if (business == null) return NotFound();
            return Ok(business);
        }

        [HttpPost]
        public async Task<ActionResult<BusinessDto>> Create([FromForm] CreateBusinessDto createDto, IFormFile? image)
        {
            Stream? stream = null;
            if (image != null)
            {
                stream = image.OpenReadStream();
            }

            var result = await _businessService.CreateAsync(createDto, stream, image?.FileName);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BusinessDto>> Update(Guid id, [FromForm] UpdateBusinessDto updateDto, IFormFile? image)
        {
            if (id != updateDto.Id) return BadRequest();

            Stream? stream = null;
            if (image != null)
            {
                stream = image.OpenReadStream();
            }

            var result = await _businessService.UpdateAsync(id, updateDto, stream, image?.FileName);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _businessService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
