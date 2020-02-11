using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using netMvcTest.Model.Entity;
using netMvcTest.Model.Entity.HelpEntity;
using WebApiTest.Api.Entities;

namespace netMvcTest.IDAL
{
    public interface IBlogCategoryService : IBaseService<BlogCategory>
    {
        Task AddBlogCategoryAsync(Guid userId, BlogCategory blogCategory);

        Task<PaginatedList<BlogCategory>> GetCategoriesAsync(Guid userId,QueryParameter queryParameter);
        Task<string[]> GetNameByCategoryIds(Guid[] ids);
        Task<List<BlogCategory>> GetBlogCategoriesByIds(Guid[] ids);


    }
}