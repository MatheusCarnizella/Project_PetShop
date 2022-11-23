using System.Linq.Expressions;

namespace Project_PetShop.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetItem();
        Task <T> GetItemById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        Task Update(T enity);
        void Delete(T entity);
    }
}
