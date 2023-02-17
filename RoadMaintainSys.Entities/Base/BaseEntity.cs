using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.Entities
{
    public class BaseEntity
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public bool IsRemoved { get; set; } = false;

        public DateTime? CreateTime { get; set; }=DateTime.Now;
    }
}
