using Microsoft.EntityFrameworkCore;
using RoadMaintainSys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.DAL.MaintainSysContext
{
    public class MaintainSysDbContext : DbContext
    {
        public MaintainSysDbContext(DbContextOptions<MaintainSysDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Admin> Admins {get;set;}
        public virtual DbSet<User> Users { get;set;}
        public virtual DbSet<News> News {get;set;}
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<TableItem> TableItems { get;set;}
    }
}
