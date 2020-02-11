using System.ComponentModel;
using Microsoft.Build.Framework;

namespace netMvcTest.Dto.User
{
    public class RegisterUserDto
    {
        [Required] [DisplayName("邮箱")] public string Email { get; set; }
        [Required][DisplayName("密码")] public string Password { get; set; }
        [Required][DisplayName("确认密码")] public string ConfirmPassword { get; set; }
    }
}