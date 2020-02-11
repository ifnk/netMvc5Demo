using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using netMvcTest.IDAL;
using netMvcTest.Model.Entity;

namespace netMvcTest.DAL
{
    public class CommentService : BaseService<Comment>, ICommentService
    {
        public CommentService() : base(new BlogContext())
        {
        }
    }
}