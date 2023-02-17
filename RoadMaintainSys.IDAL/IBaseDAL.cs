using Microsoft.Data.SqlClient;
using RoadMaintainSys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.IDAL
{
    public interface IBaseDAL<T> where T : BaseEntity
    {
        Task<int> AddOneAsync(T entity);//添加一个实体

        Task<int> AddRangeAsync(List<T> entities);//添加多个实体

        Task<int> RemoveOneByIdAsync(long id);//按id删除一个实体

        int ExecuteSql(string sql, SqlParameter[] parameters);//执行原生sql语句

        Task<int> UpdateOneAsync(T entity);//修改一个实体

        Task<IQueryable<T>> GetAllAsync();//获取所有实体

        Task<IQueryable<T>> GetOneByIdAsync(long id);//按id获取一个实体

        Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> lambda);//按条件获取实体集合

        Task<IQueryable<T>> GetAllOrderAsync(bool asc = true);//排序查询

        Task<IQueryable<T>> GetAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true);//分页查询
    }
}
