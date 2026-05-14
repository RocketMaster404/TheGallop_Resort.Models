using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public class RoomService : IRoomService
    {
        private readonly GaloppDbContext _context;

        public RoomService(GaloppDbContext context) 
        {
            _context = context;
        }

        public async Task<RoomInfoDTO> AddRoomAsync(CreateRoomDTO dto)
        {
            var categoryExists = await _context.RoomCategories
                .AnyAsync(rc => rc.Id == dto.RoomCategoryId);

            if (!categoryExists)
            {
                throw new Exception("Room category not found");
            }

            var room = new Room
            {
                RoomNr = dto.RoomNr,
                RoomCategoryId = dto.RoomCategoryId
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return new RoomInfoDTO(room.Id, room.RoomNr, room.RoomCategoryId);
        }
    }
}
