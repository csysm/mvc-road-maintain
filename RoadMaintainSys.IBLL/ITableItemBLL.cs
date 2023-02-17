using RoadMaintainSys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.IBLL
{
    public interface ITableItemBLL
    {
        Task<int> AddTableItemsByTableIdAsync(List<TableItem> entities);

        Task<int> AddRangeTableItemsByTableIdAsync(List<TableItem> entities);

        int RemoveTableItemsByTableIdAsync(long id);

        Task<int> UpdateTableItemByTableIdAsync(List<TableItem> entities);

        Task<List<TableItem>> GetTableItemsByIdAsync(long id);
    }
}
