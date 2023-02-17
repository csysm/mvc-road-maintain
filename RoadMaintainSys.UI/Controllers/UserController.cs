using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoadMaintainSys.Entities;
using RoadMaintainSys.IBLL;
using RoadMaintainSys.UI.Filters;
using RoadMaintainSys.UI.Models;
using RoadMaintainSys.UI.Utility;

namespace RoadMaintainSys.UI.Controllers
{
    [UserFilter]
    public class UserController : Controller
    {
        private IUserBLL _userBll;
        private readonly ITableBLL _tableBll;
        private readonly ITableItemBLL _tableItemBll;
        private readonly INewsBLL _newsBll;
        public UserController(IUserBLL userBll, ITableBLL tableBll, ITableItemBLL tableItemBll,INewsBLL newsBll)
        {
            _userBll = userBll;
            _tableBll = tableBll;
            _tableItemBll = tableItemBll;
            _newsBll = newsBll;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
            ViewBag.UserName = objUser.UserName;
            try
            {
                var results = await _newsBll.GetAllNewsByPageOrderAsync(0, 15);

                return View(results);
            }
            catch (Exception)
            {

                throw;
            }
        }//首页

        public async Task<IActionResult> NewsList(int pageIndex=0, int pageSize=15)//新闻列表
        {

            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
            ViewBag.UserName = objUser.UserName;
            try
            {
                var results = await _newsBll.GetAllNewsByPageOrderAsync(pageIndex, pageSize);
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

        public async Task<IActionResult> NewsDetail(long id)//新闻列表
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
            ViewBag.UserName = objUser.UserName;
            try
            {
                var news = await _newsBll.GetOneNewsByIdAsync(id);

                return View(news);
            }
            catch (Exception)
            {
                return View();
            }
        }


        [HttpGet]
        public IActionResult Privacy()
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
            ViewBag.UserName = objUser.UserName;
            
            return View(objUser);
        }//个人信息页

        
        [HttpGet]
        public async Task<IActionResult> EditUser()
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
            ViewBag.UserName = objUser.UserName;
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string objUserStr = HttpContext.Session.GetString("CurrentUser");
                User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
                if (await _userBll.Exist(Convert.ToInt64(objUser.UserId), MD5Encrypt.MD5Encrypt32(viewModel.CurrentPassword)))
                {
                    var newUser = new User()
                    {
                        Id=objUser.Id,
                        UserId=objUser.UserId,
                        UserName = viewModel.NewAdminName,
                        Password = MD5Encrypt.MD5Encrypt32(viewModel.NewPassword)
                    };
                    await _userBll.UpdateOneUserAsync(newUser);
                    return RedirectToAction("EditSuccess");//...返回成功页
                }
                else
                {
                    ModelState.AddModelError("", "密码错误");
                }
            }
            ModelState.AddModelError("", "修改失败");
            return View();
        }//修改个人信息

        
        [HttpGet]
        public async Task<IActionResult> TableList(int pageIndex = 0, int pageSize = 15)
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
            ViewBag.UserName = objUser.UserName;
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
        }//表分页

        
        [HttpGet]
        public async Task<IActionResult> TableDetails(long id,int type)//查看表详情
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
            ViewBag.UserName = objUser.UserName;
            try
            {
                var tableItems = await _tableItemBll.GetTableItemsByIdAsync(id);
                ViewBag.Type=type;
                return View(tableItems);
            }
            catch (Exception)
            {
                return View();
            }

        }

        
        [HttpGet]
        public async Task<IActionResult> SubmitCheckTable()//显示提交检查信息表页面
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
            ViewBag.UserName = objUser.UserName;

            return View();
        }



        
        [HttpPost]
        public async Task<IActionResult> SubmitCheckTable(TableCheckViewModel viewModel)//提交检查信息表
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
            

            if (ModelState.IsValid)
            {
                var table = new Table()
                {

                    Submitter = objUser.UserName,
                    TableName = "日常养护检查",
                    Type = 1
                };
                //添加一张养护信息表
                long tableId;
                _tableBll.AddOneTableAsync(table, out tableId);//添加养护信息表并取出tableId在添加表项目时使用


                //添加表项目
                
                var list = new List<TableItem>
                {
                    new TableItem(){TableId=tableId,ItemName="公路巡查",ItemContent="1",Standard="1",FinalScore=viewModel.FinalScore1,Reason=viewModel.Reason1,BaseScore=5,Remark=viewModel.Remark1},
                    new TableItem(){TableId=tableId,ItemName="桥梁经常性检查",ItemContent="2",Standard="2",FinalScore=viewModel.FinalScore2,Reason=viewModel.Reason2,BaseScore=5,Remark=viewModel.Remark2},
                    new TableItem(){TableId=tableId,ItemName="涵洞经常性检查",ItemContent="3",Standard="3",FinalScore=viewModel.FinalScore3,Reason=viewModel.Reason3,BaseScore=5,Remark=viewModel.Remark3},
                    new TableItem(){TableId=tableId,ItemName="隧道经常性检查",ItemContent="4",Standard="4",FinalScore=viewModel.FinalScore4,Reason=viewModel.Reason4,BaseScore=5,Remark=viewModel.Remark4},
                };

                int flag = await _tableItemBll.AddRangeTableItemsByTableIdAsync(list);
                if (flag == 1)
                {
                    return RedirectToAction("TableList");
                }
            }
            ModelState.AddModelError("", "添加失败");
            return View();
        }

        
        [HttpGet]
        public async Task<IActionResult> SubmitMaintainTable()//显示提交检查信息表页面
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
            ViewBag.UserName = objUser.UserName;

            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> SubmitMaintainTable(TableMaintainViewModel viewModel)//提交检查信息表
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);


            if (ModelState.IsValid)
            {
                var table = new Table()
                {

                    Submitter = objUser.UserName,
                    TableName = "日常保养维护",
                    Type = 2
                };
                //添加一张养护信息表
                long tableId;
                _tableBll.AddOneTableAsync(table, out tableId);//添加养护信息表并取出tableId在添加表项目时使用

                //添加表项目
                var list = new List<TableItem>
                {
                    new TableItem(){TableId=tableId,ItemName="路基",ItemContent="1",Standard="1",FinalScore=viewModel.FinalScore1,Reason=viewModel.Reason1,BaseScore=12,Remark=viewModel.Remark1},
                    new TableItem(){TableId=tableId,ItemName="路面",ItemContent="2",Standard="2",FinalScore=viewModel.FinalScore2,Reason=viewModel.Reason2,BaseScore=12,Remark=viewModel.Remark2},
                    new TableItem(){TableId=tableId,ItemName="桥梁",ItemContent="3",Standard="3",FinalScore=viewModel.FinalScore3,Reason=viewModel.Reason3,BaseScore=10,Remark=viewModel.Remark3},
                    new TableItem(){TableId=tableId,ItemName="涵洞",ItemContent="4",Standard="4",FinalScore=viewModel.FinalScore4,Reason=viewModel.Reason4,BaseScore=3,Remark=viewModel.Remark4},
                    new TableItem(){TableId=tableId,ItemName="隧道",ItemContent="5",Standard="5",FinalScore=viewModel.FinalScore5,Reason=viewModel.Reason5,BaseScore=3,Remark=viewModel.Remark5},
                    new TableItem(){TableId=tableId,ItemName="交通工程及沿线设施",ItemContent="6",Standard="6",FinalScore=viewModel.FinalScore6,Reason=viewModel.Reason6,BaseScore=8,Remark=viewModel.Remark6},
                    new TableItem(){TableId=tableId,ItemName="公路绿化",ItemContent="7",Standard="7",FinalScore=viewModel.FinalScore7,Reason=viewModel.Reason7,BaseScore=2,Remark=viewModel.Remark7},
                };

                int flag = await _tableItemBll.AddRangeTableItemsByTableIdAsync(list);
                if (flag == 1)
                {
                    return RedirectToAction("TableList");
                }
            }
            ModelState.AddModelError("", "添加失败");
            return View();
        }

        
        [HttpGet]
        public async Task<IActionResult> SubmitManageTable()//显示提交检查信息表页面
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);
            ViewBag.UserName = objUser.UserName;

            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> SubmitManageTable(TableManageViewModel viewModel)//提交检查信息表
        {
            string objUserStr = HttpContext.Session.GetString("CurrentUser");
            User objUser = JsonConvert.DeserializeObject<User>(objUserStr);


            if (ModelState.IsValid)
            {
                var table = new Table()
                {

                    Submitter = objUser.UserName,
                    TableName = "综合管理",
                    Type = 3
                };
                //添加一张养护信息表
                long tableId;
                _tableBll.AddOneTableAsync(table, out tableId);//添加养护信息表并取出tableId在添加表项目时使用

                //添加表项目
                var list = new List<TableItem>
                {
                    new TableItem(){TableId=tableId,ItemName="制度建设",ItemContent="1",Standard="1",FinalScore=viewModel.FinalScore1,Reason=viewModel.Reason1,BaseScore=3,Remark=viewModel.Remark1},
                    new TableItem(){TableId=tableId,ItemName="内业管理",ItemContent="2",Standard="2",FinalScore=viewModel.FinalScore2,Reason=viewModel.Reason2,BaseScore=9,Remark=viewModel.Remark2},
                    new TableItem(){TableId=tableId,ItemName="养护机械设备管理",ItemContent="3",Standard="3",FinalScore=viewModel.FinalScore3,Reason=viewModel.Reason3,BaseScore=3,Remark=viewModel.Remark3},
                    new TableItem(){TableId=tableId,ItemName="站场管理",ItemContent="4",Standard="4",FinalScore=viewModel.FinalScore4,Reason=viewModel.Reason4,BaseScore=3,Remark=viewModel.Remark4},
                    new TableItem(){TableId=tableId,ItemName="养护安全生产",ItemContent="5",Standard="5",FinalScore=viewModel.FinalScore5,Reason=viewModel.Reason5,BaseScore=4,Remark=viewModel.Remark5},
                    new TableItem(){TableId=tableId,ItemName="交通情况调查",ItemContent="6",Standard="6",FinalScore=viewModel.FinalScore6,Reason=viewModel.Reason6,BaseScore=3,Remark=viewModel.Remark6},
                    new TableItem(){TableId=tableId,ItemName="信息报送",ItemContent="7",Standard="7",FinalScore=viewModel.FinalScore7,Reason=viewModel.Reason7,BaseScore=1,Remark=viewModel.Remark7},
                    new TableItem(){TableId=tableId,ItemName="职工技术培训",ItemContent="8",Standard="8",FinalScore=viewModel.FinalScore7,Reason=viewModel.Reason7,BaseScore=1,Remark=viewModel.Remark7},
                    new TableItem(){TableId=tableId,ItemName="督办问题整改",ItemContent="9",Standard="9",FinalScore=viewModel.FinalScore7,Reason=viewModel.Reason7,BaseScore=3,Remark=viewModel.Remark7}
                };

                int flag = await _tableItemBll.AddRangeTableItemsByTableIdAsync(list);
                if (flag == 1)
                {
                    return RedirectToAction("TableList");
                }
            }
            ModelState.AddModelError("", "添加失败");
            return View();
        }

        [HttpGet]
        public IActionResult EditSuccess() //修改个人信息成功
        {
            HttpContext.Session.Clear();
            return View();
        }

        
    }
}
