using Microsoft.EntityFrameworkCore;
using Project_PetShop.Context;
using System.Linq.Expressions;

namespace Project_PetShop.Repository.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        IQueryable<T> IRepository<T>.GetItem()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetItemById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
