using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using RoadMaintainSys.Entities;
using RoadMaintainSys.IBLL;
using RoadMaintainSys.UI.Filters;
using RoadMaintainSys.UI.Models;

using RoadMaintainSys.UI.Utility;


namespace RoadMaintainSys.UI.Controllers
{
    [AdminFilter]
    public class AdminController : Controller
    {
        private IAdminBLL _adminBll;
        private ITableBLL _tableBll;
        private ITableItemBLL _tableItemBll;
        private IUserBLL _userBll;
        private INewsBLL _newsBll;
        
        public AdminController(IAdminBLL adminBll, ITableBLL tableBll, ITableItemBLL tableItemBll, IUserBLL userBll, INewsBLL newsBll)
        {
            _adminBll = adminBll;
            _tableBll = tableBll;
            _tableItemBll = tableItemBll;
            _userBll = userBll;
            _newsBll = newsBll;
            
        }
        

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
            Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
            ViewBag.AdminName = objAdmin.UserName;

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

        [HttpGet]
        public async Task<IActionResult> NewsList(int pageIndex = 0, int pageSize = 15)//新闻列表
        {

            string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
            Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
            ViewBag.AdminName = objAdmin.UserName;

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

        [HttpGet]
        public async Task<IActionResult> NewsDetail(long id)//新闻列表
        {
            string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
            Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
            ViewBag.AdminName = objAdmin.UserName;
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
        public async Task<IActionResult> RemoveNews(long id)
        {
            try
            {
                await _newsBll.RemoveOneNewsByIdAsync(id);
                return RedirectToAction("NewsList");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
            Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
            ViewBag.AdminName = objAdmin.UserName;

            return View(objAdmin);
        }//个人信息页


        [HttpGet]
        public async Task<IActionResult> EditAdmin()
        {
            string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
            Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
            ViewBag.AdminName = objAdmin.UserName;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> EditAdmin(EditAdminViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
                Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
                if (await _adminBll.Exist(Convert.ToInt64(objAdmin.UserId), MD5Encrypt.MD5Encrypt32(viewModel.CurrentPassword)))
                {
                    var newAdmin = new Admin()
                    {
                        Id = objAdmin.Id,
                        UserId = objAdmin.UserId,
                        UserName = viewModel.NewAdminName,
                        Password = MD5Encrypt.MD5Encrypt32(viewModel.NewPassword)
                    };
                    await _adminBll.UpdateOneAdminAsync(newAdmin);

                    return RedirectToAction("EditSuccess");//...返回成功页
                }
                else
                {
                    ModelState.AddModelError("", "密码错误");
                }
            }
            ModelState.AddModelError("", "修改失败");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> TableList(int pageIndex = 0, int pageSize = 15)
        {
            string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
            Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
            ViewBag.AdminName = objAdmin.UserName;
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
        public async Task<IActionResult> ExportExcel(int pageIndex=0, int pageSize=15)//导出Excel
        {
            //创建文件
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("结果导出");
            List<TableItem> results = await _tableBll.ExportTableItem(pageIndex, pageSize);
            worksheet.Cells.LoadFromCollection(results, true);
            string filePath = @"wwwroot\Export\output.xlsx";//在wwwroot的Export文件夹中生成文件
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            package.SaveAs(fileStream);
            fileStream.Close();
            
            //返回文件
            string contentType = "application/vnd.ms-excel";
            return File(@"Export\output.xlsx", contentType);//return File()返回给前端文件会默认去wwwroot文件夹下寻找,所以不需要写wwwroot\
        }




        [HttpGet]
        public async Task<IActionResult> TableDetails(long id, int type)//查看表详情
        {
            string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
            Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
            ViewBag.AdminName = objAdmin.UserName;
            try
            {
                var tableItems = await _tableItemBll.GetTableItemsByIdAsync(id);
                ViewBag.Type = type;
                return View(tableItems);
            }
            catch (Exception)
            {
                return View();
            }

        }


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


        [HttpGet]
        public async Task<IActionResult> UserList(int pageIndex = 0, int pageSize = 15)
        {
            string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
            Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
            ViewBag.AdminName = objAdmin.UserName;
            try
            {
                var results = await _userBll.GetAllUserByPageOrderAsync(pageIndex, pageSize);
                var userCount = await _userBll.GetUserCount();
                ViewBag.PageCount = userCount % pageSize == 0 ? userCount / pageSize : userCount / pageSize + 1;
                ViewBag.PageIndex = pageIndex;
                return View(results);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserDetail(long id)
        {
            string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
            Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
            ViewBag.AdminName = objAdmin.UserName;

            try
            {
                var user = await _userBll.GetOneUserByIdAsync(id);

                return View(user);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt64(DateTime.Now.ToString("ydffff"));
                var user = new User()
                {
                    UserId = userId,
                    UserName = viewModel.UserName,
                    Password = MD5Encrypt.MD5Encrypt32(viewModel.Password)
                };
                await _userBll.AddOneUserAsync(user);
                return RedirectToAction("UserList");//...返回成功页
            }
            ModelState.AddModelError("", "添加失败");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> RemoveUser(long id)//删除用户
        {
            try
            {
                await _userBll.RemoveOneUserByIdAsync(id);//删除用户
                return RedirectToAction("UserList");
            }
            catch (Exception)
            {

                return View("Error");
            }
        }


        [HttpGet]
        public async Task<IActionResult> AdminList(int pageIndex = 0, int pageSize = 15)
        {
            try
            {
                var results = await _adminBll.GetAllAdminByPageOrderAsync(pageIndex, pageSize);
                var adminCount = await _adminBll.GetAdminCount();

                ViewBag.PageCount = adminCount % pageSize == 0 ? adminCount / pageSize : adminCount / pageSize + 1;
                ViewBag.PageIndex = pageIndex;
                return View(results);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        

        [HttpGet]
        public async Task<IActionResult> AddNews()
        {
            string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
            Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
            ViewBag.AdminName = objAdmin.UserName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNews(AddNewsViewModel viewModel)
        {
            string objAdminStr = HttpContext.Session.GetString("CurrentAdmin");
            Admin objAdmin = JsonConvert.DeserializeObject<Admin>(objAdminStr);
            ViewBag.AdminName = objAdmin.UserName;

            if (ModelState.IsValid)
            {
                var news = new News()
                {
                    Title = viewModel.Title,
                    Author=objAdmin.UserName,
                    Content = viewModel.Content
                };
                await _newsBll.AddOneNewsAsync(news);//添加养护信息表并取出tableId在添加表项目时使用
                return RedirectToAction("NewsList");
            }
            ModelState.AddModelError("", "添加失败");
            return View();
        }

        [HttpGet]
        public IActionResult EditSuccess()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpGet]
        public IActionResult AddSuccess(long userId)
        {
            ViewBag.AdminName = userId.ToString();
            return View();
        }

        /*
        #region 添加管理员
        [AdminFilter]
        [HttpGet]
        public IActionResult AddAdmin()
        {

            return View();
        }

        [AdminFilter]
        [HttpPost]
        public async Task<IActionResult> AddAdmin(AddAdminViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt64(DateTime.Now.ToString("ddhhmmss"));
                var admin = new Admin()
                {
                    UserId = userId,
                    UserName = viewModel.UserName,
                    Password = viewModel.Password
                };
                await _adminBll.AddOneAdminAsync(admin);
                return RedirectToAction("AddSuccess", new { userId = userId });//...返回成功页
            }
            ModelState.AddModelError("", "添加失败");
            return View();
        }
        #endregion
        */
    }
}
