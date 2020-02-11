using System.ComponentModel;
using Microsoft.Build.Framework;

namespace netMvcTest.Dto.User
{
    public class UpdateUserDto
    {
        
        [Required] [DisplayName("邮箱")] public string Email { get; set; }
        [Required][DisplayName("密码")] public string Password { get; set; }

        //头像
        [DisplayName("用户头像")]
        public string ImagePath { get; set; }
    }
}