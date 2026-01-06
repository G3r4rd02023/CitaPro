using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CP.Server.Services;
using CP.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAll()
        {
            return Ok(await _reservationService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetById(Guid id)
        {
            var result = await _reservationService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("business/{businessId}")]
        [Authorize(Roles = "Business, Admin")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetByBusiness(Guid businessId)
        {
            // Simple check: if not admin, should check if user owns business
            return Ok(await _reservationService.GetByBusinessIdAsync(businessId));
        }

        [HttpGet("mine")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetMyReservations()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);
            return Ok(await _reservationService.GetByUserIdAsync(userId));
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDto>> Create(CreateReservationDto createDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            createDto.UserId = Guid.Parse(userIdClaim.Value);
            var result = await _reservationService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Business, Admin")]
        public async Task<ActionResult<ReservationDto>> UpdateStatus(Guid id, UpdateReservationDto updateDto)
        {
            if (id != updateDto.Id) return BadRequest();

            var result = await _reservationService.UpdateStatusAsync(id, updateDto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _reservationService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
