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

        async Task<IQueryable<T>> IRepository<T>.GetItem()
        {
            return _context.Set<T>().AsNoTracking();
        }

        async Task<T> IRepository<T>.GetItemById(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        async Task IRepository<T>.Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        async Task IRepository<T>.Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }

        async Task IRepository<T>.Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
