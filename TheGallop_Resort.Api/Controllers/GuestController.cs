using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : Controller
    {

        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

       

        [HttpPost("/Guest")]
        public async Task<IActionResult>AddGuest(CreateGuestDTO dto)
        {
            var guest = await _guestService.AddGuestAsync(dto);
            return Ok(guest);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllGuestsInfo()
        {
            var guests = await _guestService.GetAllGuestsInfoAsync();

            

            return Ok(guests);
        }

        [HttpGet("{guestId}")]
        public async Task<IActionResult> GetGuestInfoById(int guestId)
        {
            var guest = await _guestService.GetGuestInfoByIdAsync(guestId);

            return Ok(guest);
        }

        [HttpDelete("{guestId}")]
        public async Task<IActionResult> DeleteGuest(int guestId)
        {
            var guestToDelete = await _guestService.DeleteGuestAsync(guestId);

            

            return NoContent();

        }
    }
}
