using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.Entities
{
    public class TableItem:BaseEntity
    {
        public long TableId { get; set; }
        public Table Table { get; set; }

        [MaxLength(20)]
        public string? ItemName { get; set; }

        [MaxLength(200)]
        public string? ItemContent { get; set; }

        public int? BaseScore { get; set; }

        [MaxLength(100)]
        public string? Standard { get; set; }

        [MaxLength(100)]
        public string?  Reason { get; set; }

        public double? FinalScore { get; set; }

        [MaxLength(50)]
        public string? Remark { get; set; }
    }
}
