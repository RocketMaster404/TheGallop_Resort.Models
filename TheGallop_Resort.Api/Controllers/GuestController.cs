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

        [HttpPost]
        public async Task<IActionResult>AddGuest(CreateGuestDTO dto)
        {
            var guest = new Guest
            {
                FirstName = dto.FirstName,
                LastName = dto.lastName,
                Email = dto.Email,
                PhoneNumber = dto.Phone
            };

            await _ctx.Guests.AddAsync(guest);
            await _ctx.SaveChangesAsync();

            return Ok(guest);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllGuestsInfo()
        {
            var guests = await _ctx.Guests.ToListAsync();

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

            return Ok(guest);
        }
    }
}
