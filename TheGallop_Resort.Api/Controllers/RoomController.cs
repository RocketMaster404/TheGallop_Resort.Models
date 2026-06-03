using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : BaseController
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("getAllRooms", Name = "getAllRooms")]
        public async Task<ActionResult<IEnumerable<GetRoomResponseDTO>>> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();

            return Ok(rooms.Data);
        }
    }
}
