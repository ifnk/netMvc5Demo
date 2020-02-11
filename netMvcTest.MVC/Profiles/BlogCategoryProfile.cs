using netMvcTest.Model.Entity;

namespace netMvcTest.MVC.Profiles
{
    public class BlogCategoryProfile : AutoMapper.Profile
    {

        public BlogCategoryProfile()
        {
            CreateMap<BlogCategory, BlogCategoryDto>();
            CreateMap<BlogCategory, AddBlogCategoryDto>();
            CreateMap<AddBlogCategoryDto, BlogCategory>();
        }
    }
}