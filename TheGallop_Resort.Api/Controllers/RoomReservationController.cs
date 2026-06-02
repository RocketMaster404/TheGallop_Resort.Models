using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;

namespace TheGallop_Resort.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomReservationController : BaseController
    {
        private readonly IRoomReservationService _roomReservationService;
        private IValidator<CreateRoomReservationDTO> _createRoomReservationDTO;

        public RoomReservationController(IRoomReservationService roomReservationService, IValidator<CreateRoomReservationDTO> createRoomReservationDTO)
        {
            _roomReservationService = roomReservationService;
            _createRoomReservationDTO = createRoomReservationDTO;
        }

        [HttpPost("CreateRoomReservation", Name = "CreateRoomReservation")]
        public async Task<ActionResult<GetFullBookingResponsDTO>> CreateRoomReservation(CreateRoomReservationDTO dto)
        {
            var validation = await _createRoomReservationDTO.ValidateAsync(dto);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var roomReservation = await _roomReservationService.CreateRoomReservationAsync(dto);

            if (!roomReservation.SuccessfulResult)
            {
                return BadRequest(roomReservation);
            }

            return Ok(roomReservation.Data);
        }
    }
}
