using System.Threading.Tasks;
using netMvcTest.Dto;
using netMvcTest.Model.Entity;

namespace netMvcTest.IBLL
{
    public interface IUserManager
    {
        //注册 
        Task Register(User user);

        //登录  
        Task<bool> Login(User user);

        // //修改密码  
        // Task ChangePassword(User user);
        //
        // //修改用户个人信息  
        // Task ChangeUserInfo(User user);
        //
        // //根据 email 获取 用户 信息   
        // Task<UserDto> GetUserByEmail(User user);
    }
}