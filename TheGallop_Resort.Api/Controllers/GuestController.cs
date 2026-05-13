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

        //[HttpGet]
        //public async Task<IActionResult> GetAllGuestsInfo()
        //{
        //    var guests = await _ctx.Guests.ToListAsync();

        //    if(guests.Count == 0)
        //    {
        //        return NotFound("No Guests found");
        //    }

        //    return Ok(guests);
        //}

        //[HttpGet("{guestId}")]
        //public async Task<IActionResult> GetGuestInfoById(int guestId)
        //{
        //    var guest = await _ctx.Guests.Where(g => g.Id == guestId).Select(g => new GuestInfoDTO(
        //        g.FirstName,
        //        g.LastName,
        //        g.Email,
        //        g.PhoneNumber
        //        )).FirstOrDefaultAsync();

        //    if(guest == null)
        //    {
        //        return NotFound("Guest not found");
        //    }

        //    return Ok(guest);
        //}

        //[HttpDelete("{guestId}")]
        //public async Task<IActionResult> DeleteGuest(int guestId)
        //{
        //    var guestToDelete = await _ctx.Guests.FirstOrDefaultAsync(g => g.Id == guestId);

        //    if(guestToDelete == null)
        //    {
        //        return NotFound("Guest not found");
        //    }

        //    _ctx.Guests.Remove(guestToDelete);
        //    await _ctx.SaveChangesAsync();

        //    return NoContent();

        //}
    }
}
