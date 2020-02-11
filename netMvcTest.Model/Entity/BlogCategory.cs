using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netMvcTest.Model.Entity
{
    //博客分类表
    public class BlogCategory : BaseEntity
    {
        //分类名称
        [Required] public string Name { get; set; }


        //用户id 
        [ForeignKey(nameof(User))] public Guid UserId { get; set; }

        //用户 entity 
        public User User { get; set; }
        public List<ArticleToCategory> ArticleToCategories { get; set; }
    }
}