using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public interface IRoomCategoryService
    {
        Task<ServiceResult<RoomCategory>> AddRoomCategoryAsync(CreateRoomCategoryDTO dto);
        Task<IEnumerable<RoomCategory>> GetAllRoomCategoriesAsync();
        Task<ServiceResult<RoomCategory>> GetRoomCategoryByIdAsync(int roomCategoryId);
        Task<ServiceResult> DeleteRoomCategoryAsync(int roomCategoryId);


    }
}
