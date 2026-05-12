using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : Controller
    {
        private readonly GaloppDbContext _ctx;

        public GuestController(GaloppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost("/Guest")]
        public async Task<IActionResult>AddGuest(CreateGuestDTO dto)
        {
            var guest = new Guest
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.Phone
            };

            await _ctx.Guests.AddAsync(guest);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGuestInfoById), new { guestId = guest.Id }, guest);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllGuestsInfo()
        {
            var guests = await _ctx.Guests.ToListAsync();

            if(guests.Count == 0)
            {
                return NotFound("No Guests found");
            }

            return Ok(guests);
        }

        [HttpGet("{guestId}")]
        public async Task<IActionResult> GetGuestInfoById(int guestId)
        {
            var guest = await _ctx.Guests.Where(g => g.Id == guestId).Select(g => new GuestInfoDTO(
                g.FirstName,
                g.LastName,
                g.Email,
                g.PhoneNumber
                )).FirstOrDefaultAsync();

            if(guest == null)
            {
                return NotFound("Guest not found");
            }

            return Ok(guest);
        }

        [HttpDelete("{guestId}")]
        public async Task<IActionResult> DeleteGuest(int guestId)
        {
            var guestToDelete = await _ctx.Guests.FirstOrDefaultAsync(g => g.Id == guestId);

            if(guestToDelete == null)
            {
                return NotFound("Guest not found");
            }

            _ctx.Guests.Remove(guestToDelete);
            await _ctx.SaveChangesAsync();

            return NoContent();

        }
    }
}
