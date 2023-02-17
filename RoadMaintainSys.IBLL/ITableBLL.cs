using RoadMaintainSys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.IBLL
{
    public interface ITableBLL
    {
        int AddOneTableAsync(Table entity,out long id);

        Task<int> RemoveOneTableByIdAsync(long id);

        Task<int> UpdateOneTableAsync(Table entity);

        Task<Table> GetOneTableByIdAsync(long id);

        Task<List<Table>> GetAllTableByPageOrderAsync(int pageIndex, int pageSize);

        Task<int> GetTableCount();

        Task<List<TableItem>> ExportTableItem(int pageIndex, int pageSize);
    }
}
