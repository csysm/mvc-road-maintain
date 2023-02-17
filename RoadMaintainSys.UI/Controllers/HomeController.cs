using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoadMaintainSys.IBLL;
using RoadMaintainSys.UI.Models;
using RoadMaintainSys.UI.Utility;
using System.Diagnostics;

namespace RoadMaintainSys.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminBLL _adminBll;
        private readonly IUserBLL _userBll;

        public HomeController(ILogger<HomeController> logger, IAdminBLL adminBll, IUserBLL userBll)
        {
            _adminBll=adminBll;
            _userBll=userBll;
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AdminLogin()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(AdminLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //查询数据库
                var objAdmin = await _adminBll.AdminLogin(Convert.ToInt64(viewModel.UserId), MD5Encrypt.MD5Encrypt32(viewModel.Password));
                //若存在该用户则将该用户信息保存到Session
                if (objAdmin != null)
                {
                    HttpContext.Session.SetString("CurrentAdmin", JsonConvert.SerializeObject(objAdmin));
                    //存在返回"ok"
                    return RedirectToAction("Index", "Admin");
                }
                //若不存在用户则返回"no"
                ModelState.AddModelError("","用户名或密码错误");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserLogin()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //查询数据库
                var objUser = await _userBll.UserLogin(Convert.ToInt64(viewModel.UserId), MD5Encrypt.MD5Encrypt32(viewModel.Password));
                //若存在该用户则将该用户信息保存到Session
                if (objUser != null)
                {
                    HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(objUser));
                    //存在返回"ok"
                    return RedirectToAction("Index", "User");
                }
                //若不存在用户则返回"no"
                ModelState.AddModelError("", "用户名或密码错误");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return new RedirectResult("/Home/Index");
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}