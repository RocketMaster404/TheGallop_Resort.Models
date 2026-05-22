using System.ComponentModel.DataAnnotations;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.DTOs
{
    public record RoomCategoryDTO
    {
        public RoomType Type { get; init; }

        [Range(0, double.MaxValue, ErrorMessage = "Price can't be negative")]
        public decimal CategoryPrice { get; init; }

        public int RoomDetailId { get; init; }
    }
}
