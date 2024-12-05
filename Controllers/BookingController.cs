using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using temuruang_be.Dtos.BookingDTO;
using temuruang_be.Models;
using temuruang_be.Services;

namespace temuruang_be.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookSvc;
    private readonly ILogger<BookingController> _logger;

    public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
    {
        _bookSvc = bookingService;
        _logger = logger;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateBooking(CreateBookDTO dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

            if (userId == null)
            {
                return Unauthorized(ApiResponse<string?>.Create(401, "unauthorized", null));
            }

            dto.UserId = Guid.Parse(userId);

            var booking = await _bookSvc.CreateBooking(dto);

            return Ok(ApiResponse<Booking>.Create(200, "successfully create booking", booking));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateBooking(int id, UpdateBookDTO dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

            if (userId == null)
            {
                return Unauthorized(ApiResponse<string?>.Create(401, "unauthorized", null));
            }

            var res = await _bookSvc.UpdateBooking(id, dto);

            if (!res)
            {
                return NotFound(ApiResponse<string?>.Create(404, "failed to delete booking", null));
            }
            return Ok(ApiResponse<Booking?>.Create(200, "successfully update booking", null));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

            if (userId == null)
            {
                return Unauthorized(ApiResponse<string?>.Create(401, "unauthorized", null));
            }

            var res = await _bookSvc.DeleteBooking(id, Guid.Parse(userId));

            if (!res)
            {
                return NotFound(ApiResponse<string?>.Create(404, "failed to delete booking", null));
            }

            return Ok(ApiResponse<Booking?>.Create(200, "successfully delete booking", null));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

    }
}