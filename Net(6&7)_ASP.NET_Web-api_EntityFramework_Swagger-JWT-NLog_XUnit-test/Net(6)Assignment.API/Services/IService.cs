using System.Linq.Expressions;

namespace Net_6_Assignment.Services
{
    public interface IService<T> where T : class
    {
        // get all
        Task<IEnumerable<T>> GetAllAsync();
        // create
        Task<T> CreateAsync(T entity);
        // get by id
        Task<T?> GetByConditionAsync(Expression<Func<T, bool>> predicate);
        // update by id
        Task<T> UpdateAsync(T existingEntity, T updatedEntity);
        // delete by id
        Task<bool> DeleteAsync(T entity);
    }
}
