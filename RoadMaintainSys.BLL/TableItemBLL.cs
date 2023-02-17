using Microsoft.Data.SqlClient;
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
    public class TableItemBLL : ITableItemBLL
    {
        private ITableItemDAL _tableItemDal;
        public TableItemBLL(ITableItemDAL tableItemDal)
        {
            _tableItemDal = tableItemDal;
        }

        public async Task<int> AddTableItemsByTableIdAsync(List<TableItem> entities)//增

        {
            try
            {
                //List<Task<int>> tasks = new List<Task<int>>();
                foreach (var entity in entities)
                {
                    //tasks.Add(_tableItemDal.AddOneAsync(entity));
                    await _tableItemDal.AddOneAsync(entity);
                }
                //await Task.WhenAll(tasks);
                //return tasks.Count;
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddRangeTableItemsByTableIdAsync(List<TableItem> entities)//增
        {
            try
            {
               await _tableItemDal.AddRangeAsync(entities);
               return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int RemoveTableItemsByTableIdAsync(long tableId)
        {
            try
            {
                string sql = $"UPDATE TableItems SET ItemName = null,ItemContent = null,BaseScore = 0,Standard = null,Reason = null,FinalScore = 0,Remark = null,IsRemoved = 1,CreateTime = null WHERE TableId = @tableId";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@tableId",tableId)
                };
                return _tableItemDal.ExecuteSql(sql, parameters);
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }//删


        public Task<int> UpdateTableItemByTableIdAsync(List<TableItem> entities)
        {
            throw new NotImplementedException();
        }//改

        public async Task<List<TableItem>> GetTableItemsByIdAsync(long id)//查
        {
            var result = await _tableItemDal.GetByConditionAsync(e => e.TableId == id);
            return result.ToList();
        }
    }
}
