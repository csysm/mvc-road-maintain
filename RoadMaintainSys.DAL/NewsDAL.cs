using Microsoft.EntityFrameworkCore;
using RoadMaintainSys.Entities;
using RoadMaintainSys.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.DAL
{
    public class NewsDAL:BaseDAL<News>,INewsDAL
    {
        public NewsDAL(DbContext context)
        {
            _context = context;
        }
    }
}
