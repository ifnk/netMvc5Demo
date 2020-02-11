using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netMvcTest.Model.Entity
{
    public class Article : BaseEntity
    {
        //标题 
        [Required] public string Title { get; set; }

        //内容
        [Required] public string Content { get; set; }

        //评论数量
        public int CommentCount { get; set; }

        //点赞数量
        public int LikeCount { get; set; } = 0;

        //反对数量
        public int OpposeCount { get; set; } = 0;

        //用户id 
        [ForeignKey(nameof(User))] public Guid UserId { get; set; }

        //用户 entity 
        public User User { get; set; }


        public List<ArticleToCategory> ArticleToCategories { get; set; }
    }
}