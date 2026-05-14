using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : Controller
    {
        private readonly GaloppDbContext _galoppDbContext;
        public RoomsController(GaloppDbContext galoppDbContext)
        {
            _galoppDbContext = galoppDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Room>> CreateRoom(CreateRoomDTO dto)
        {

            //Checking that RoomCategory exists
            var roomCategoryExists = await _galoppDbContext.RoomCategories.AnyAsync(rc => rc.Id == dto.RoomCategoryId);

            if (!roomCategoryExists) 
            {
                ModelState.AddModelError(nameof(dto.RoomCategoryId), "Room category does not exist.");
                return ValidationProblem(ModelState);
            }

            var room = new Room
            {
                RoomNr = dto.RoomNr,
                RoomCategoryId = dto.RoomCategoryId
            };

            _galoppDbContext.Rooms.Add(room);
            await _galoppDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoomById), new { id = room.Id },
                new RoomInfoDTO(room.Id, room.RoomNr, room.RoomCategoryId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoomById(int id)
        {
            var room = await _galoppDbContext.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
            
        }

    }
}
