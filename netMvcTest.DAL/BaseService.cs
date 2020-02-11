using System;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using netMvcTest.IDAL;
using netMvcTest.Model.Entity;

namespace netMvcTest.DAL
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        protected readonly BlogContext _context;

        public BaseService(BlogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(T model, bool saved = true)
        {
            _context.Set<T>().Add(model);
            if (saved) await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T model, bool saved = true)
        {
            _context.Configuration.ValidateOnSaveEnabled = false; // 修改之前先关闭校验
            _context.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                await _context.SaveChangesAsync();
                _context.Configuration.ValidateOnSaveEnabled = true; // 修改之后 开启 校验
            }
        }

        public async Task RemoveAsync(Guid id, bool saved = true)
        {
            _context.Configuration.ValidateOnSaveEnabled = false; // 修改之前先关闭校验
            var t = new T() {Id = id};
            _context.Entry(t).State = EntityState.Unchanged;
            t.IsRemoved = true;
            if (saved)
            {
                await _context.SaveChangesAsync();
                _context.Configuration.ValidateOnSaveEnabled = true; // 修改之后 开启 校验
            }
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
            await RemoveAsync(model.Id, saved = true);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
            _context.Configuration.ValidateOnSaveEnabled = true; // 修改之后 开启 校验
        }

        public async Task<T> GetOneByIdAsync(Guid id)
        {
            return await GetAll().FirstAsync(x => x.Id == id);
        }

        //这时候 返回 的是 queryable 查询表达式 ,没有真的执行查询
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>()
                .Where(x => !x.IsRemoved)
                .AsQueryable();
        }

        public IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0)
        {
            return GetAll().Skip(pageSize * pageIndex)
                .Take(pageSize);
        }

        //销毁
        public void Dispose()
        {
            //释放sql 连接 
            _context?.Dispose();
        }
    }
}