using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public class RoomCategoryService : IRoomCategoryService
    {
        private readonly GaloppDbContext _ctx;
        public RoomCategoryService(GaloppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ServiceResult<RoomCategory>> AddRoomCategoryAsync(RoomCategoryDTO dto)
        {
            var roomDetailExists = await _ctx.RoomDetails
                .AnyAsync(rd => rd.Id == dto.RoomDetailId);
            if (!roomDetailExists)
            {
                return ServiceResult<RoomCategory>.ValidationError("Room Detail not found");
            }

            var roomCategory = new RoomCategory
            {
                Type = dto.Type,
                CategoryPrice = dto.CategoryPrice,
                RoomDetailId = dto.RoomDetailId
            };

            await _ctx.RoomCategories.AddAsync(roomCategory);
            await _ctx.SaveChangesAsync();

            return ServiceResult<RoomCategory>.Ok(roomCategory);
        }

        public async Task<IEnumerable<RoomCategory>> GetAllRoomCategoriesAsync()
        {
            return await _ctx.RoomCategories
                .Include(rc => rc.RoomDetail)
                .ToListAsync();
        }
        public async Task<ServiceResult<RoomCategory>> GetRoomCategoryByIdAsync(int roomCategoryId)
        {
            var roomCategory = await _ctx.RoomCategories.Include(rc => rc.RoomDetail).FirstOrDefaultAsync(rc => rc.Id == roomCategoryId);
            
            if (roomCategory == null)
            {
                return ServiceResult<RoomCategory>.NotFound("Room category not found");
            }

            return ServiceResult<RoomCategory>.Ok(roomCategory);
        }

        public async Task<ServiceResult> DeleteRoomCategoryAsync(int roomCategoryId)
        {
            var roomCategory = await _ctx.RoomCategories
                .FirstOrDefaultAsync(rc => rc.Id == roomCategoryId);

            if (roomCategory == null)
            {
                return ServiceResult.NotFound("Room category not found");

            }

            _ctx.RoomCategories.Remove(roomCategory);
            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> UpdateRoomCategoryAsync(int roomCategoryId, RoomCategoryDTO dto)
        {
            var roomCategory = await _ctx.RoomCategories.FirstOrDefaultAsync(rc => rc.Id == roomCategoryId);

            if (roomCategory == null)
            {
                return ServiceResult.NotFound("Room Category not found");
            }

            var roomDetailExists = await _ctx.RoomDetails
                    .AnyAsync(rd => rd.Id == dto.RoomDetailId);

            if (!roomDetailExists)
            {
                return ServiceResult.ValidationError("Room Detail not found");
            }

            roomCategory.Type = dto.Type;
            roomCategory.CategoryPrice = dto.CategoryPrice;
            roomCategory.RoomDetailId = dto.RoomDetailId;

            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }
    }
}
