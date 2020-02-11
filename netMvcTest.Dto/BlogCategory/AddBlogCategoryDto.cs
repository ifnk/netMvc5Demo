using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace netMvcTest.Model.Entity
{
    public class AddBlogCategoryDto
    {
        //分类名称
        [Required] [DisplayName("分类名称")] public string Name { get; set; }
    }
}