using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.Entities
{
    public class Table:BaseEntity
    {
        public int? Type { get; set; }//1-3

        [MaxLength(20)]
        public string? TableName { get; set; }

        [MaxLength(20)]
        public string? Submitter { get; set; }

    }
}
