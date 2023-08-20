using Microsoft.EntityFrameworkCore;
using Pschool.Data;

namespace Pschool.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> CreateAsync(T entity)
        {
            var result=await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var res = await _dbContext.Set<T>().FindAsync(id);
            if(res != null)
            {
                _dbContext.Set<T>().Remove(res);
                await _dbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);

        }

        public async Task<IEnumerable<T>> Search(string name)
        {
            IQueryable<T> values = _dbContext.Set<T>();
            if (!string.IsNullOrEmpty(name))
            {
                values=values.Where(m=>m.Equals(name));

            }
            return await values.ToListAsync();
                
        }

        public async Task<T> UpdateAsync(int id)
        {
            var res = await _dbContext.Set<T>().FindAsync(id);
            if (res != null)
            {
                _dbContext.Set<T>().Update(res);
                await _dbContext.SaveChangesAsync();
                return res;
            }
            return null;

        }
    }
}
