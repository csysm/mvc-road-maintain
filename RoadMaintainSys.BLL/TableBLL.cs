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
    public class TableBLL : ITableBLL
    {
        private ITableDAL _tableDal;
        private ITableItemDAL _tableItemDal;

        public TableBLL(ITableDAL tableDal, ITableItemDAL tableItemDal)
        {
            _tableDal = tableDal;
            _tableItemDal = tableItemDal;
        }

        public int AddOneTableAsync(Table entity, out long id)
        {
            try
            {
                _tableDal.AddOneAsync(entity).Wait();
                id = entity.Id;
                return 1;
            }
            catch (Exception)
            {
                id = -1;
                return -1;
            }
        }

        public async Task<Table> GetOneTableByIdAsync(long id)
        {
            var result = await _tableDal.GetOneByIdAsync(id);

            return result.FirstOrDefault();
        }

        public async Task<int> RemoveOneTableByIdAsync(long id)
        {
            return await _tableDal.RemoveOneByIdAsync(id);
        }

        public async Task<int> UpdateOneTableAsync(Table entity)
        {
            return await _tableDal.UpdateOneAsync(entity);
        }

        public async Task<List<Table>> GetAllTableByPageOrderAsync(int pageIndex, int pageSize)
        {
            var result = await _tableDal.GetAllByPageOrderAsync(pageIndex, pageSize, false);
            return result.ToList();
        }

        public async Task<int> GetTableCount()
        {
            var result = await _tableDal.GetAllAsync();
            return result.Count();
        }

        public async Task<List<TableItem>> ExportTableItem(int pageIndex, int pageSize)
        {
            List<TableItem> results=new List<TableItem>();
            var tables = await _tableDal.GetAllByPageOrderAsync(pageIndex, pageSize, false);
            var tableIds= tables.Select(t => t.Id).ToList();
            foreach (var tableId in tableIds)
            {
                var tableItems= await _tableItemDal.GetByConditionAsync(item => item.TableId == tableId);
                foreach (var item in tableItems)
                {
                    results.Add(item);
                }
            }

            return results;
        }

    }
}
