using RoadMaintainSys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RoadMaintainSys.IBLL
{
    public interface IAdminBLL
    {
        Task<int> AddOneAdminAsync(Admin entity);

        Task<int> RemoveOneAdminByIdAsync(long id);

        Task<int> UpdateOneAdminAsync(Admin entity);

        Task<Admin> GetOneAdminByIdAsync(long id);

        Task<List<Admin>> GetAllAdminByPageOrderAsync(int pageIndex, int pageSize);

        Task<bool> Exist(long userId, string password);

        Task<int> GetAdminCount();

        Task<Admin> AdminLogin(long userId, string password);
    }
}
