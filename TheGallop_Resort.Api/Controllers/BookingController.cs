using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.DTOs.Validators;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;
        private IValidator<UpdateBookingStatusDTO> _updateBookingStatusDTO;

        private IValidator<UpdateBookingGuestDTO> _updateBookingGuestDTO;
        private IValidator<GetInputFromUserCreateDTO> _getInputFromUserCreateDTO;


        public BookingController(IBookingService bookingService, IValidator<UpdateBookingStatusDTO> updateBookingStatusDTO, IValidator<UpdateBookingGuestDTO> updateBookingGuestDTO, IValidator<GetInputFromUserCreateDTO> getInputFromUserCreateDTO)
        {
            _bookingService = bookingService;
            _updateBookingStatusDTO = updateBookingStatusDTO;
            _updateBookingGuestDTO = updateBookingGuestDTO;
            _getInputFromUserCreateDTO = getInputFromUserCreateDTO;
        }

        [HttpGet("getAllBookings", Name = "GetAllBooking")]
        public async Task<ActionResult<IEnumerable<GetBookingResponseDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();

            return Ok(bookings.Data);
        }

        [HttpGet("getBookingsById/{bookingId}", Name = "getBookingsById")]
        public async Task<ActionResult<GetBookingResponseDTO>> GetBookingById(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);

            if (!booking.SuccessfulResult)
            {
                return NotFound(booking.ErrorMessage);
            }

            return Ok(booking.Data);
        }

        [HttpPost("CreateBooking", Name = "CreateBooking")]
        //DTO
        public async Task<ActionResult<GetFullBookingResponsDTO>>CreateBooking(GetInputFromUserCreateDTO dto)
        {
            var validation = await _getInputFromUserCreateDTO.ValidateAsync(dto);

            if (!validation.IsValid)
            {
                return BadRequest();
            }

            var result = await _bookingService.CreateBookingAsync(dto);

            if (!result.SuccessfulResult)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result);
        }

        [HttpPut("updateGuestOnBooking", Name = "UpdateGuestOnBooking")]
        public async Task<IActionResult> UpdateGuestOnBooking(UpdateBookingGuestDTO update)
        {

            var validation = await _updateBookingGuestDTO.ValidateAsync(update);

            if (!validation.IsValid)
            {
                return BadRequest();
            }

            var booking = await _bookingService.UpdateGuestOnBookingAsync(update);

            if (!booking.SuccessfulResult)
            {
                return ToErrorResponse(booking);
            }

            return Ok(booking);
        }

        [HttpPut("updateStatusOnBooking", Name = "UpdateStausOnBooking")]
        public async Task<IActionResult> UpdateBookingStatus(UpdateBookingStatusDTO update)
        {
            var validation = await _updateBookingStatusDTO.ValidateAsync(update);

            if (!validation.IsValid)
            {
                return BadRequest();
            }

            var booking = await _bookingService.UpdateBookingStatusAsync(update);

            if (!booking.SuccessfulResult)
            {
                return ToErrorResponse(booking);
            }

            return NoContent();
        }

        [HttpGet("getBookingsForNextMonth", Name = "getBookingsForNextMonth")]
        public async Task<ActionResult<IEnumerable<GetBookingResponseDTO>>> GetBookingsForNextMonth()
        {
            var bookings = await _bookingService.GetBookingsForNextMonthAsync();

            return Ok(bookings.Data);
        }

        [HttpGet("GetBookingsForSpecifikDate", Name = "GetBookingsForSpecifikDate")]
        public async Task<ActionResult<IEnumerable<GetBookingResponseDTO>>> GetBookingsForSpecifikDate(DateOnly inputDate)
        {
            var bookings = await _bookingService.GetBookingsForSpecifikDateAsync(inputDate);

            return Ok(bookings.Data);
        }
    }
}
