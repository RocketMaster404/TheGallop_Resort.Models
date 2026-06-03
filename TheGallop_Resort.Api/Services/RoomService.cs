using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;

namespace TheGallop_Resort.Api.Services
{
    public class RoomService : IRoomService
    {
        private readonly GaloppDbContext _ctx;

        public RoomService(GaloppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ServiceResult<IEnumerable<GetRoomResponseDTO>>> GetAllRoomsAsync()
        {
            var rooms = await _ctx.Rooms
                .AsNoTracking()
                .Select(r => new GetRoomResponseDTO(
                    r.Id,
                    r.RoomNr,
                    r.RoomCategory.Type
                    )).ToListAsync();

            if (rooms.Count == 0)
            {
                return ServiceResult<IEnumerable<GetRoomResponseDTO>>.NotFound("No rooms were found");
            }

            return ServiceResult<IEnumerable<GetRoomResponseDTO>>.Ok(rooms);
        }
    }
}
