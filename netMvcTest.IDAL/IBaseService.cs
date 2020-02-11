using System;
using System.Linq;
using System.Threading.Tasks;
using netMvcTest.Model.Entity;

namespace netMvcTest.IDAL
{
    public interface IBaseService<T> : IDisposable where T : BaseEntity
    {
        //创建 
        Task AddAsync(T model, bool saved = true);

        //修改 
        Task UpdateAsync(T model,bool saved=true);

        //删除  根据 id  
        Task RemoveAsync(Guid id,bool saved=true);

        //删除 对象   
        Task RemoveAsync(T model,bool saved=true);
        Task SaveAsync();
        

        //获取 一个 根据 id 
        Task<T> GetOneByIdAsync(Guid id);

        //获取 获取多个(列表)
        IQueryable<T> GetAll();

        //分页
        IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0);

    }
}