using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.Entities
{
    public class News :BaseEntity
    {
        [MaxLength(50)]
        public string? Title { get; set; }

        [MaxLength(20)]
        public string? Author { get; set; }

        [MaxLength(500)]
        public string? Content { get; set; }
    }
}
