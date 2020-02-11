using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netMvcTest.Model.Entity
{
    public class User : BaseEntity
    {
        [Required, StringLength(40)] public string Email { get; set; }

        [Required, StringLength(30)] public string Password { get; set; }

        //头像
        public string ImagePath { get; set; } = "default.png";

    }
}