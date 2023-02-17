using RoadMaintainSys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.IBLL
{
    public interface IUserBLL
    {
        Task<int> AddOneUserAsync(User entity);

        Task<int> RemoveOneUserByIdAsync(long id);

        Task<int> UpdateOneUserAsync(User entity);

        Task<User> GetOneUserByIdAsync(long id);

        Task<List<User>> GetAllUserByPageOrderAsync(int pageIndex, int pageSize);

        Task<int> GetUserCount();

        Task<User> UserLogin(long userId, string password);

        Task<bool> Exist(long userId, string password);
    }
}
