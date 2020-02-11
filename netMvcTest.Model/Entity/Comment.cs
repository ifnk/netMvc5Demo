using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netMvcTest.Model.Entity
{
    //评论
    public class Comment : BaseEntity
    {
        //用户id 
        [ForeignKey(nameof(User))] public Guid UserId { get; set; }
        public User User { get; set; }


        //文章id 
        [ForeignKey(nameof(Article))] public Guid ArticleId { get; set; }
        public Article Article { get; set; }

       

        //评论内容
        [Required] public string Content { get; set; }
    }
}