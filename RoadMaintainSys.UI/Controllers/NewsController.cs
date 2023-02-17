using Microsoft.AspNetCore.Mvc;
using RoadMaintainSys.IBLL;

namespace RoadMaintainSys.UI.Controllers
{
    public class NewsController : Controller
    {
        private INewsBLL _newsBll;

        public NewsController(INewsBLL newsBll)
        {
            _newsBll = newsBll;
        }

        public async Task<IActionResult> NewsList(int pageIndex,int pageSize)
        {
            try
            {
                var results = await _newsBll.GetAllNewsByPageOrderAsync(pageIndex,pageSize);
                var newsCount = await _newsBll.GetNewsCount();
                ViewBag.PageCount = newsCount % pageSize == 0 ? newsCount / pageSize : newsCount / pageSize + 1;
                ViewBag.PageIndex = pageIndex;
                return View(results);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> NewsListIndex()//获取最新15条新闻
        {
            try
            {
                var results = await _newsBll.GetAllNewsByPageOrderAsync(0, 15);
                
                return View(results);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
