using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using netMvcTest.Bll;
using netMvcTest.Dto.User;
using netMvcTest.IBLL;
using netMvcTest.IDAL;
using netMvcTest.Model.Entity;
using netMvcTest.MVC.Filters;

namespace netMvcTest.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        // autofac属性注入
        public HomeController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public ActionResult Index()
        {
            return View();
        }

        [BlogFilterAuth]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [BlogFilterAuth]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //注册
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = Mapper.Map<User>(registerUserDto);
            // var user = new User() {Email = registerUserDto.Email, Password = registerUserDto.Password};
            // IUserManager userManager = new UserManager();    
            await _userService.AddAsync(user);
            return Content("注册成功!");
            // return View(registerUserDto);
        }

        //登录
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //登录
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid) return View(loginUserDto);
            var user = await _userService.GetOneByEmail(loginUserDto.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "邮箱不存在!");
                return View(loginUserDto);
            }

            if (user.Password == loginUserDto.Password)
            {
                // 登录成功
                //跳转  判断 是 用session 和 cookie
                // cookie的数据信息存放在客户端浏览器上。
                // session的数据信息存放在服务器上。
                //点击 记住我就  存cookie  不点就存 session 
                if (loginUserDto.RememberMe) // 用户点击 了 记住我
                {
                    Response.Cookies.Add(new HttpCookie("userName")
                    {
                        Value = user.Email, // 保存用户的邮箱当作 登录名
                        Expires = DateTime.Now.AddDays(7) // 保存7天
                    });
                    Response.Cookies.Add(new HttpCookie("userId")
                    {
                        Value = user.Id.ToString(), // 保存用户的邮箱当作 登录名
                        Expires = DateTime.Now.AddDays(7) // 保存7天
                    });
                }
                else
                {
                    Session["userId"] = user.Id.ToString();
                    Session["userName"] = user.Email;
                }

                return RedirectToAction(nameof(Index)); // 跳转到首页
            }

            ModelState.AddModelError("", "密码不正确!");
            return View(loginUserDto);
        }

        //退出登录
        [HttpGet]
        [BlogFilterAuth]
        public ActionResult Logout()
        {
            if (Session["userId"] != null && Session["userName"] != null)
            {
                Session.Remove("userId");
                Session.Remove("userName");
            }

            //这里是删除 不掉cookie 的，必须使用 让他过期的方法 才行，就 是 减天数
            //(设置多少天就减掉,他就自动过期了)
            if (HttpContext.Request.Cookies.AllKeys.Contains("userName"))
            {
                var cookie = HttpContext.Request.Cookies["userName"];
                cookie.Expires = DateTime.Now.AddDays(-7);
                HttpContext.Response.Cookies.Add(cookie);
            }

            if (HttpContext.Request.Cookies.AllKeys.Contains("userId"))
            {
                var cookie = HttpContext.Request.Cookies["userId"];
                cookie.Expires = DateTime.Now.AddDays(-7);
                HttpContext.Response.Cookies.Add(cookie);
            }

            return RedirectToAction(nameof(Login)); // 跳转到首页
        }

        //更改用户信息
        [HttpGet]
        [BlogFilterAuth]
        public ActionResult changeUser()
        {
            return View();
        }

        // [HttpPost]
        // public ActionResult changeUser()
        // {
        //     return View();
        // }
    }
}