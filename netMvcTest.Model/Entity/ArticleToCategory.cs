using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace netMvcTest.Model.Entity
{
    //文章和分类之前的多对多关系 
    public class ArticleToCategory : BaseEntity

    {
        [ForeignKey(nameof(Entity.BlogCategory))]
        public Guid BlogCategoryId { get; set; }

        public BlogCategory BlogCategory { get; set; }


        [ForeignKey(nameof(Entity.Article))] 
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}