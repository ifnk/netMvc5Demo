using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using netMvcTest.IDAL;
using netMvcTest.Model.Entity;
using netMvcTest.Model.Entity.HelpEntity;
using WebApiTest.Api.Entities;

namespace netMvcTest.DAL
{
    public class BlogCategoryService : BaseService<BlogCategory>, IBlogCategoryService
    {
        public BlogCategoryService() : base(new BlogContext())
        {
        }

        public async Task AddBlogCategoryAsync(Guid userId, BlogCategory blogCategory)
        {
            blogCategory.UserId = userId;
            await AddAsync(blogCategory);
        }

        public async Task<PaginatedList<BlogCategory>> GetCategoriesAsync(Guid userId, QueryParameter queryParameter)
        {
            if (queryParameter == null) throw new ArgumentNullException(nameof(queryParameter));
            var query = _context.BlogCategories
                .Include(x => x.ArticleToCategories)
                .Where(x => !x.IsRemoved && x.UserId == userId)
                .OrderBy(x => x.CreateTime)
                .AsQueryable();
            if (!string.IsNullOrEmpty(queryParameter.Key))
            {
                query = query.Where(x => x.Name.Contains(queryParameter.Key));
            }

            return await PaginatedList<BlogCategory>.CreatePaginateQueryAsync(query, queryParameter.PageIndex,
                queryParameter.PageSize);
        }

        //根据 一串 id 获取 多个 姓名 array
        public async Task<string[]> GetNameByCategoryIds(Guid[] ids)
        {
            if (ids == null) throw new ArgumentNullException(nameof(ids));
            return await _context.BlogCategories
                .Where(x => ids.Contains(x.Id))
                .Select(x => x.Name)
                .ToArrayAsync();
        }

        //根据 一串 id 获取 多个 blogCategory list
        public async Task<List<BlogCategory>> GetBlogCategoriesByIds(Guid[] ids)
        {
            if (ids == null) throw new ArgumentNullException(nameof(ids));
            return await _context.BlogCategories
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }
    }
}