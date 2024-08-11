using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce_Project.Contexts;
using Mini_E_Commerce_Project.Models.Common;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Mini_E_Commerce_Project.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly AppDBContext _context;

        public Repository()
        {
            _context = new AppDBContext();
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync(params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();  //selecting and piling

            foreach(var include in includes) 
            {
                query = query.Include(include);
            }

            var result = await query.ToListAsync(); //listing them all up

            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var product = await _context.Set<T>().AsQueryable().AsNoTracking().FirstOrDefaultAsync();
            return product;
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _context.Set<T>().AnyAsync(predicate);

            return result;
        }
    }
}
