using Microsoft.AspNetCore.Mvc;

namespace RoadMaintainSys.UI.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
