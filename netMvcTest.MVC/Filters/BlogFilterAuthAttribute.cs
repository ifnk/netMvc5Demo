using System.Web.Mvc;
using System.Web.Routing;

namespace netMvcTest.MVC.Filters
{
    //拦截器
    public class BlogFilterAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // cookie的数据信息存放在客户端浏览器上。
            // session的数据信息存放在服务器上。
            // 假如 session 没存 cookie 存了
            // 就把 cookie 里面 的内容 存到 session 里面 
            if (filterContext.HttpContext.Request.Cookies["userName"] != null
                &&
                filterContext.HttpContext.Session["userName"] == null)
            {
                filterContext.HttpContext.Session["userName"] = filterContext.HttpContext.Request.Cookies["userName"].Value;
                filterContext.HttpContext.Session["userId"] = filterContext.HttpContext.Request.Cookies["userId"].Value;
            }

            // base.OnAuthorization(filterContext);
            // 检测 有没有 存 session 或者 cookie
            if (!(filterContext.HttpContext.Session["userName"] != null ||
                  filterContext.HttpContext.Request.Cookies["userName"] != null))
            {
                // 没有的 话就 跳转 到 登录 页面 
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"controller", "Home"},
                    {"action", "Login"}
                });
            }
        }
    }
}