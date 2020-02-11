using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using netMvcTest.IDAL;
using netMvcTest.Model.Entity;

namespace netMvcTest.DAL
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService() : base(new BlogContext())
        {
        }

        //用户邮箱是否存在
        public async Task<bool> ExistEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User> GetOneByEmail(string email)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));
            return await _context.Users
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}