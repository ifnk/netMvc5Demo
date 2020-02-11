using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using netMvcTest.Bll;
using netMvcTest.DAL;
using netMvcTest.IBLL;
using netMvcTest.IDAL;

namespace netMvcTest.MVC
{
    public class AutoFacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            //注册MVC控制器（注册所有到控制器，控制器注入，就是需要在控制器的构造函数中接收对象）
            // builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            /*接口实现*/
            // user 类
            builder.RegisterType<UserService>().As<IUserService>();
            
            //article 类
            builder.RegisterType<BlogCategoryService>().As<IBlogCategoryService>();
            builder.RegisterType<ArticleService>().As<IArticleService>();
            builder.RegisterType<ArticleToCategoryService>().As<IArticleToCategoryService>();
            
            //comment 类
            builder.RegisterType<CommentService>().As<ICommentService>();
            
            //设置依赖解析器
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}