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
    public class UserDAL:BaseDAL<User>,IUserDAL
    {
        public UserDAL(DbContext context)
        {
            _context = context;
        }
    }
}
