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
    public class AdminBLL : IAdminBLL
    {
        private IAdminDAL _adminDal;
        public AdminBLL(IAdminDAL adminDal)
        {
            _adminDal = adminDal;
        }

        public async Task<int> AddOneAdminAsync(Admin entity)
        {
            return await _adminDal.AddOneAsync(entity);
        }

        public async Task<int> RemoveOneAdminByIdAsync(long id)
        {
            return await _adminDal.RemoveOneByIdAsync(id);
        }

        public async Task<int> UpdateOneAdminAsync(Admin entity)
        {
            return await _adminDal.UpdateOneAsync(entity);
        }

        public async Task<Admin> GetOneAdminByIdAsync(long id)
        {
            var result= await _adminDal.GetOneByIdAsync(id);
            return result.FirstOrDefault();
        }

        public async Task<List<Admin>> GetAllAdminByPageOrderAsync(int pageIndex, int pageSize)
        {
            var result = await _adminDal.GetAllByPageOrderAsync(pageIndex,pageSize);
            return result.ToList();
        }

        public async Task<bool> Exist(long userId, string password)
        {
            var result= await _adminDal.GetAllAsync();
            return await result.AnyAsync(e => e.UserId == userId && e.Password == password);
        }

        public async Task<int> GetAdminCount()
        {
            var result = await _adminDal.GetAllAsync();
            return result.Count();
        }

        public async Task<Admin> AdminLogin(long userId,string password)
        {
            var result = await _adminDal.GetByConditionAsync(e => e.UserId == userId && e.Password == password);
            return await result.FirstOrDefaultAsync();
            
        }
    }
}
