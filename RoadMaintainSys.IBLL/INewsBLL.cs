using RoadMaintainSys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadMaintainSys.IBLL
{
    public interface INewsBLL
    {
        Task<int> AddOneNewsAsync(News entity);

        Task<int> RemoveOneNewsByIdAsync(long id);

        Task<News> GetOneNewsByIdAsync(long id);

        Task<List<News>> GetAllNewsByPageOrderAsync(int pageIndex, int pageSize);

        Task<int> GetNewsCount();
    }
}
