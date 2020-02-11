using System.ComponentModel;
using Microsoft.Build.Framework;

namespace netMvcTest.Dto.User
{
    public class LoginUserDto
    {
        [Required] [DisplayName("邮箱")] public string Email { get; set; }

        [Required] [DisplayName("密码")] public string Password { get; set; }

        [DisplayName("记住我")] public bool RememberMe { get; set; }
    }
}