using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RoadMaintainSys.Entities;
using RoadMaintainSys.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.DAL
{
    public class BaseDAL<T> : IBaseDAL<T> where T : BaseEntity, new()
    {
        protected DbContext _context;

        public async Task<int> AddOneAsync(T entity)//添加一个实体
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception)
            {
                throw;
                
            }
        }

        public async Task<int> AddRangeAsync(List<T> entities)//批量添加实体
        {
            try
            {
                await _context.Set<T>().AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> RemoveOneByIdAsync(long id)//按id删除一个实体(假删除)
        {
            try
            {
                T entity = new()
                {
                    Id = id,
                    CreateTime = null,
                    IsRemoved = true,
                };
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public int ExecuteSql(string sql,SqlParameter[] parameters)//执行sql语句查询
        {

            return _context.Database.ExecuteSqlRaw(sql,parameters);
        }

        public async Task<int> UpdateOneAsync(T entity)//按实体(必须含id)更新一个实体
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.Run(() => _context.Set<T>().Where(e => e.IsRemoved == false).AsNoTracking());
        }

        public async Task<IQueryable<T>> GetOneByIdAsync(long id)
        {
            var results = await GetByConditionAsync(e => e.IsRemoved == false);
            return results.Where(e => e.Id == id).AsNoTracking();
        }

        public async Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> lambda)//条件查询
        {
            var results = await GetAllAsync();
            return results.Where(lambda).AsNoTracking();
        }

        public async Task<IQueryable<T>> GetAllOrderAsync(bool asc = true)//按照CreateTime排序获取所有实体(默认时间从早到晚排序)
        {
            var datas = await GetAllAsync();
            if (asc)
            {
                datas = datas.OrderBy(e => e.CreateTime);
            }
            else
            {
                datas = datas.OrderByDescending(e => e.CreateTime);
            }
            return datas;
        }

        public async Task<IQueryable<T>> GetAllByPageOrderAsync(int pageIndex,int pageSize,bool asc = true)//分页查询
        {
            var result= await GetAllOrderAsync(asc);
            return result.Skip((pageIndex)*pageSize ).Take(pageSize);
        }

    }
}
