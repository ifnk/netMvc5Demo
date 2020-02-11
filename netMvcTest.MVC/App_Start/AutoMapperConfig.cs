using AutoMapper;
using netMvcTest.MVC.Profiles;

namespace netMvcTest.MVC
{
    public class AutoMapperConfig
    {
        public static void LoadConfig()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<ArticleProfile>();
                cfg.AddProfile<BlogCategoryProfile>();
            });
        }
    }
}