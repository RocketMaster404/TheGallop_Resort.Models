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

        public RoomReservationController(IRoomReservationService roomReservationService)
        {
            _roomReservationService = roomReservationService;
        }

        [HttpPost("CreateRoomReservation", Name = "CreateRoomReservation")]
        public async Task<ServiceResult<CreateRoomReservationDTO>> CreateRoomReservation(CreateRoomReservationDTO dto)
        {
            //var validation = await _.ValidateAsync(dto);

            //if (!validation.IsValid)
            //{
            //    return BadRequest();
            //}

            var result = await _roomReservationService.CreateRoomReservationAsync(dto);

            if (!result.SuccessfulResult)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
