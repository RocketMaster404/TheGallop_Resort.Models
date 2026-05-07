using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGallop_Resort.Models.Models
{
    public class RoomDetail
    {
        public int Id { get; set; }

        [Range(0,3)]
        public int Beds { get; set; }

        public List<string> Amenities { get; set; } = new List<string>();
    }
}
