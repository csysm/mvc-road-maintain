using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.Entities
{
    public class Admin:BaseEntity
    {
        public long? UserId { get; set; }

        [MaxLength(32)]
        public string? Password { get; set; }

        [MaxLength(20)]
        public string? UserName { get; set; }

        public int? IsAdmin { get; set; } = 1;

    }
}
