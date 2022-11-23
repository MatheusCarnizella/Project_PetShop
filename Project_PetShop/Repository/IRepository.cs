using System.Linq.Expressions;

namespace Project_PetShop.Repository
{
    public interface IRepository<T>
    {
        Task <IQueryable<T>> GetItem();
        Task <T> GetItemById(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task Update(T enity);
        Task Delete(T entity);
    }
}
