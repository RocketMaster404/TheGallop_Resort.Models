using TheGallop_Resort.Api.DTOs;

namespace TheGallop_Resort.Api.Services
{
    public interface IRoomService
    {
        Task<ServiceResult<IEnumerable<GetRoomResponseDTO>>> GetAllRoomsAsync();
    }
}
