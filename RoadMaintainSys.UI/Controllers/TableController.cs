using Microsoft.AspNetCore.Mvc;
using RoadMaintainSys.Entities;
using RoadMaintainSys.IBLL;
using RoadMaintainSys.UI.Filters;
using RoadMaintainSys.UI.Models;
using Webdiyer.AspNetCore;

namespace RoadMaintainSys.UI.Controllers
{
    
    public class TableController : Controller
    {
        private ITableBLL _tableBll;
        private ITableItemBLL _tableItemBll;
        public TableController(ITableBLL tableBll, ITableItemBLL tableItemBll)
        {
            _tableBll = tableBll;
            _tableItemBll = tableItemBll;
        }

        [UserFilter]
        [HttpGet]
        public async Task<IActionResult> SubmitCheckTable()//显示提交检查信息表页面
        {


            return View();
        }

        [UserFilter]
        [HttpPost]
        public async Task<IActionResult> SubmitCheckTable(TableCheckViewModel viewModel)//提交检查信息表
        {
            if (ModelState.IsValid)
            {
                var table = new Table()
                {
                    Submitter = "wxx",//从session中取出
                    TableName = "日常养护检查",
                    Type = 1
                };
                //添加一张养护信息表
                long tableId;
                _tableBll.AddOneTableAsync(table, out tableId);//添加养护信息表并取出tableId在添加表项目时使用

                //添加表项目
                var list = new List<TableItem>
                {
                    new TableItem(){TableId=tableId,ItemName="公路巡查",FinalScore=viewModel.FinalScore1,Reason=viewModel.Reason1,BaseScore=5,Remark=viewModel.Remark1},
                    new TableItem(){TableId=tableId,ItemName="桥梁经常性检查",FinalScore=viewModel.FinalScore2,Reason=viewModel.Reason2,BaseScore=5,Remark=viewModel.Remark2},
                    new TableItem(){TableId=tableId,ItemName="涵洞经常性检查",FinalScore=viewModel.FinalScore3,Reason=viewModel.Reason3,BaseScore=5,Remark=viewModel.Remark3},
                    new TableItem(){TableId=tableId,ItemName="隧道经常性检查",FinalScore=viewModel.FinalScore4,Reason=viewModel.Reason4,BaseScore=5,Remark=viewModel.Remark4},
                };

                int flag = await _tableItemBll.AddTableItemsByTableIdAsync(list);
                if (flag == 1)
                {
                    return RedirectToAction("TableList");
                }
            }
            ModelState.AddModelError("", "添加失败");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TableDetails(long id)//详情
        {
            try
            {
                var tableItems = await _tableItemBll.GetTableItemsByIdAsync(id);

                return View(tableItems);
            }
            catch (Exception)
            {
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> TableList(int pageIndex = 0, int pageSize = 10)
        {
            try
            {
                var results = await _tableBll.GetAllTableByPageOrderAsync(pageIndex, pageSize);
                var tableCount = await _tableBll.GetTableCount();
                ViewBag.PageCount = tableCount % pageSize == 0 ? tableCount / pageSize : tableCount / pageSize + 1;
                ViewBag.PageIndex = pageIndex;
                return View(results);
            }
            catch (Exception)
            {

                throw;
            }
        }//分页

        [AdminFilter]
        [HttpGet]
        public async Task<IActionResult> RemoveTable(long id)//删除养护信息表
        {
            try
            {
                await _tableBll.RemoveOneTableByIdAsync(id);//删除养护信息表
                int num = _tableItemBll.RemoveTableItemsByTableIdAsync(id);//删除表项目

                return RedirectToAction("TableList");
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
    }
}
