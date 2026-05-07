using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGallop_Resort.Models.Models
{
    public class RoomCategory
    {
        public int Id { get; set; }
        public RoomType Type { get; set; }
        [Range(0,double.MaxValue,ErrorMessage = "Price can't be negative")]
        public decimal AddedPrice { get; set; }
        public int RoomDetailsId { get; set; }
        public RoomDetail RoomDetails { get; set; } = null!;

        public ICollection<Room> Rooms { get; set; } = new List<Room>();

    }

    public enum RoomType
    {
        Single,
        Double,
        Suite
    }
}
