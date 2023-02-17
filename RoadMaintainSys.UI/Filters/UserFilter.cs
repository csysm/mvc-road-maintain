using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RoadMaintainSys.UI.Filters
{
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //若Session不存在,则不允许进入隐私页,返回登陆页面
            if (context.HttpContext.Session.GetString("CurrentUser") == null)
            {
                context.Result = new RedirectResult("/Home/UserLogin");
            }
        }
    }
}
