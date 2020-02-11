using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace netMvcTest.Dto.Article
{
    public class ArticleDto
    {
        //标题 
        [DisplayName("标题")] public string Title { get; set; }
        public Guid Id { get; set; }

        //内容
        [DisplayName("内容")] public string Content { get; set; }

        [DisplayName("点赞")] public int LikeCount { get; set; } = 0;

        [DisplayName("反对")]public int OpposeCount { get; set; } = 0;

        //评论数量
        [DisplayName("评论数")] public int CommentCount { get; set; }
        [DisplayName("作者")] public string UserEmail { get; set; }
        [DisplayName("文章分类")] public Guid[] CategoryIds { get; set; }
        [DisplayName("文章分类")] public string[] CategoryNames { get; set; }
    }
}