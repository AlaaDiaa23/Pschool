namespace Pschool.Repository.Base
{
    public interface IBaseRepository<T>where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(int id);
        Task<T> DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> Search(string name);

    }
}
