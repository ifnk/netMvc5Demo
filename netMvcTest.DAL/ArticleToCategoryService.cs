﻿using netMvcTest.IDAL;
using netMvcTest.Model.Entity;

namespace netMvcTest.DAL
{
    public class ArticleToCategoryService : BaseService<ArticleToCategory>, IArticleToCategoryService
    {
        public ArticleToCategoryService() : base(new BlogContext())
        {
        }
    }
}