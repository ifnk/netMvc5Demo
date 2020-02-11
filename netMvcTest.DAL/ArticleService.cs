using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using netMvcTest.IDAL;
using netMvcTest.Model.Entity;
using netMvcTest.Model.Entity.HelpEntity;
using WebApiTest.Api.Entities;
using Z.EntityFramework.Plus;

namespace netMvcTest.DAL
{
    public class ArticleService : BaseService<Article>, IArticleService
    {
        public ArticleService() : base(new BlogContext())
        {
        }

        public async Task AddArticleAsync(Guid userId, Article article)
        {
            article.UserId = userId;
            await AddAsync(article);
        }


        public async Task<Article> GetArticleAsync(Guid id)
        {
            return id == Guid.Empty
                ? throw new ArgumentNullException(nameof(id))
                : await GetAll()
                    .IncludeFilter(x => x.ArticleToCategories.Where(i => !i.IsRemoved))
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PaginatedList<Article>> GetArticlesAsync(QueryParameter queryParameter)
        {
            if (queryParameter == null) throw new ArgumentNullException(nameof(queryParameter));

            var query = _context.Articles
                .Include(x => x.User)
                .Include(x => x.ArticleToCategories)
                .Where(x => !x.IsRemoved)
                .OrderBy(x => x.CreateTime)
                .AsQueryable();
            if (!string.IsNullOrEmpty(queryParameter.Key))
            {
                query = query.Where(x =>
                    x.Title.Contains(queryParameter.Key) || x.Content.Contains(queryParameter.Key));
            }


            return await PaginatedList<Article>.CreatePaginateQueryAsync(query, queryParameter.PageIndex,
                queryParameter.PageSize);
        }
    }
}