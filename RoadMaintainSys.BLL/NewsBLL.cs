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
    public class NewsBLL:INewsBLL
    {
        private INewsDAL _newsDal;
        public NewsBLL(INewsDAL newsDal)
        {
            _newsDal = newsDal;
        }

        public async Task<int> AddOneNewsAsync(News entity)
        {
            return await _newsDal.AddOneAsync(entity);
        }

        public async Task<int> RemoveOneNewsByIdAsync(long id)
        {
            return await _newsDal.RemoveOneByIdAsync(id);
        }

        public async Task<News> GetOneNewsByIdAsync(long id)
        {
            var result = await _newsDal.GetOneByIdAsync(id);
            return result.FirstOrDefault();
        }


        public async Task<List<News>> GetAllNewsByPageOrderAsync(int pageIndex, int pageSize)
        {
            var result = await _newsDal.GetAllByPageOrderAsync(pageIndex, pageSize,false);
            return result.ToList();
        }

        public async Task<int> GetNewsCount()
        {
            var result = await _newsDal.GetAllAsync();
            return result.Count();
        }
    }
}
