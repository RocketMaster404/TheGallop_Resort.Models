using FluentValidation;
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
    public class GuestController : BaseController
    {

        private readonly IGuestService _guestService;
        private IValidator<CreateGuestDTO> _createGuestValidator;

        //public GuestController(IGuestService guestService)
        //{
        //    _guestService = guestService;
        //}

        public GuestController(IGuestService guestService, IValidator<CreateGuestDTO> createGuestValidator)
        {
            _guestService = guestService;
            _createGuestValidator = createGuestValidator;
        }

        [HttpGet("{guestId}/GuestBookingHistory")]
        public async Task<IActionResult> GetUsersBookingHistory(int guestId)
        {
            var guest = await _guestService.GetUserBookingHistoryAsync(guestId);

            if(!guest.SuccessfulResult)
            {
                return NotFound("Guest not found");
            }

            return Ok(guest);
        }
        [HttpGet("{guestId}/GuestFutureREservations")]
        public async Task<ActionResult<List<GetBookingResponseDTO>>> GetGuestFutureBookings(int guestId)
        {
            var guest = await _guestService.GetGuestFutureBookingsAsync(guestId);

            if (!guest.SuccessfulResult)
            {
                return BadRequest(guest.ErrorMessage);
            }

            return Ok(guest);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGuestsInfo()
        {
            var guests = await _guestService.GetAllGuestsInfoAsync();



            return Ok(guests);
        }

       


        [HttpPost("/Guest")]
        public async Task<IActionResult>AddGuest(CreateGuestDTO dto)
        {

            var validation = await _createGuestValidator.ValidateAsync(dto);

            if (!validation.IsValid)
            {
                return BadRequest();
            }


            var guest = await _guestService.AddGuestAsync(dto);

            if (!guest.SuccessfulResult)
            {
                return BadRequest(guest.ErrorMessage);
            }

            return Ok(guest.Data);

        }

     
        [HttpGet("{guestId}")]
        public async Task<IActionResult> GetGuestInfoById(int guestId)
        {
            var guest = await _guestService.GetGuestInfoByIdAsync(guestId);

            if (!guest.SuccessfulResult)
            {
                return NotFound("Guest not found");
            }

            return Ok(guest.Data);
        }

        [HttpDelete("{guestId}")]
        public async Task<IActionResult> DeleteGuest(int guestId)
        {
            var guestToDelete = await _guestService.DeleteGuestAsync(guestId);

            if (!guestToDelete.SuccessfulResult)
            {
                return NotFound("Guest not found");
            }

            return NoContent();

        }

        [HttpPut("{guestId}")]
        public async Task<IActionResult> UpdateGuestInfo(int guestId,GuestInfoDTO dto)
        {
            var guest = await _guestService.UpdateGuestInfoAsync(guestId, dto);

            if (!guest.SuccessfulResult)
            {
                return BadRequest(guest.ErrorMessage);
            }

            return NoContent();
        }
    }
}
