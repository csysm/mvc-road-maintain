using RoadMaintainSys.IDAL;
using RoadMaintainSys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RoadMaintainSys.DAL
{
    public class TableItemDAL:BaseDAL<TableItem>,ITableItemDAL
    {
        public TableItemDAL(DbContext context)
        {
            _context = context;
            
        }

        
    }
}
