using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;

namespace TheGallop_Resort.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomCategoryController : Controller
    {
        private readonly IRoomCategoryService _roomCategoryService;

        public RoomCategoryController(IRoomCategoryService roomCategoryService)
        {
            _roomCategoryService = roomCategoryService;
        }

        // POST api/<RoomCategoryController>
        [HttpPost]
        public async Task<IActionResult> AddRoomCategory([FromBody] RoomCategoryDTO dto)
        {
            var result = await _roomCategoryService.AddRoomCategoryAsync(dto);

            if(!result.SuccessfulResult)
            {
                if (result.Status == ServiceResultStatus.ValidationError)
                {
                    ModelState.AddModelError(nameof(dto.RoomDetailId), result.ErrorMessage!);
                    return ValidationProblem(ModelState);
                }

                    return NotFound(result.ErrorMessage);
                
            }

            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoomCategories()
        {
            var roomCategories = await _roomCategoryService.GetAllRoomCategoriesAsync();
            
            return Ok(roomCategories);
        }

        [HttpGet("{roomCategoryId}")]
        public async Task<IActionResult> GetRoomCategoryById(int roomCategoryId)
        {
            var result = await _roomCategoryService.GetRoomCategoryByIdAsync(roomCategoryId);

            if (!result.SuccessfulResult)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }


        [HttpPut("{roomCategoryId}")]
        public async Task<IActionResult> UpdateRoomCategory(int roomCategoryId, RoomCategoryDTO dto)
        {
            var result = await _roomCategoryService.UpdateRoomCategoryAsync(roomCategoryId, dto);

            if (!result.SuccessfulResult)
            {
                if (result.Status == ServiceResultStatus.ValidationError)
                {
                    ModelState.AddModelError(nameof(dto.RoomDetailId), result.ErrorMessage!);
                    return ValidationProblem(ModelState);
                }

                return NotFound(result.ErrorMessage);
            }

            return NoContent();
        }

        // DELETE api/<RoomCategoryController>/5
        [HttpDelete("{roomCategoryId}")]
        public async Task<IActionResult> DeleteRoomCategory(int roomCategoryId)
        {
            var result = await _roomCategoryService.DeleteRoomCategoryAsync(roomCategoryId);
            
            if (!result.SuccessfulResult)
            {
                return NotFound(result.ErrorMessage);
            }

            return NoContent();
        }
    }
}
