using System.Threading.Tasks;
using netMvcTest.Model.Entity;

namespace netMvcTest.IDAL
{
    public interface IUserService : IBaseService<User>
    {
        //用户邮箱是否存在
        Task<bool> ExistEmailAsync(string email);
        
        //根据 邮箱查询用户 
        Task<User> GetOneByEmail(string email);
        
    }
}