using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using netMvcTest.DAL;
using netMvcTest.IBLL;
using netMvcTest.IDAL;
using netMvcTest.Model.Entity;

namespace netMvcTest.Bll
{
    public class UserManager : IUserManager
    {
        //autoFac 依赖注入
        public IUserService _userService { get; set; }

        public async Task Register(User user)
        {
            await _userService.AddAsync(user);
        }

        public async Task<bool> Login(User user)
        {
            return await _userService.GetAll().AnyAsync(x => x.Email == user.Email && x.Password == user.Password);
        }
    }
}