using System;
using System.ComponentModel.DataAnnotations;

namespace netMvcTest.Model.Entity
{
    public class BlogCategoryDto
    {
        //分类名称
         public string Name { get; set; }
         public Guid Id { get; set; }
    }
}