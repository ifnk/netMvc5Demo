using System;
using System.ComponentModel;
using Microsoft.Build.Framework;

namespace netMvcTest.Dto.Article
{
    public class EditArticleDto
    {
        //标题 
        [Required] [DisplayName("标题")] public string Title { get; set; }

        //内容
        [Required] [DisplayName("内容")] public string Content { get; set; }

        [Required] [DisplayName("分类名称")] public Guid[] CategoryIds { get; set; }
    }
}