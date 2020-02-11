using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using netMvcTest.Model.Entity;
using netMvcTest.Model.Entity.HelpEntity;
using WebApiTest.Api.Entities;

namespace netMvcTest.IDAL
{
    public interface IArticleService : IBaseService<Article>
    {
        Task<Article> GetArticleAsync(Guid id);
        Task AddArticleAsync(Guid userId, Article article);

        Task<PaginatedList<Article>> GetArticlesAsync( QueryParameter queryParameter);
    }
}