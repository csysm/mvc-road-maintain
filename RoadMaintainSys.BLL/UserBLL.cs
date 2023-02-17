using Microsoft.EntityFrameworkCore;
using RoadMaintainSys.Entities;
using RoadMaintainSys.IBLL;
using RoadMaintainSys.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.BLL
{
    public class UserBLL : IUserBLL
    {
        private IUserDAL _userDal;
        public UserBLL(IUserDAL userDal)
        {
            _userDal = userDal;
        }

        public async Task<int> AddOneUserAsync(User entity)
        {
            return await _userDal.AddOneAsync(entity);
        }

        public async Task<int> RemoveOneUserByIdAsync(long id)
        {
            return await _userDal.RemoveOneByIdAsync(id);
        }

        public async Task<int> UpdateOneUserAsync(User entity)
        {
            return await _userDal.UpdateOneAsync(entity);
        }

        public async Task<User> GetOneUserByIdAsync(long id)
        {
            var result = await _userDal.GetOneByIdAsync(id);
            return result.FirstOrDefault();
        }

        public async Task<List<User>> GetAllUserByPageOrderAsync(int pageIndex, int pageSize)
        {
            var result = await _userDal.GetAllByPageOrderAsync(pageIndex, pageSize,false);
            return result.ToList();
        }


        public async Task<bool> Exist(long userId, string password)
        {
            var result = await _userDal.GetAllAsync();
            return await result.AnyAsync(e=>e.UserId==userId&&e.Password==password);
        }

        public async Task<int> GetUserCount()
        {
            var result = await _userDal.GetAllAsync();
            return result.Count();
        }

        public async Task<User> UserLogin(long userId, string password)
        {
            var result = await _userDal.GetByConditionAsync(e => e.UserId == userId && e.Password == password);
            return await result.FirstOrDefaultAsync();

        }
    }
}
