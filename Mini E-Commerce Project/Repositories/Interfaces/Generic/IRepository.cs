using Mini_E_Commerce_Project.Models.Common;
using System.Linq.Expressions;

namespace Mini_E_Commerce_Project.Repositories.Interfaces.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync(params string[] includes);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    }
}
