using System.Linq.Expressions;

namespace DAL.Common.Interface
{
    public interface IRepository<TEntity, T> where TEntity : BaseEntity<T>
    {
        Task<TEntity?> GetById(T id);

        Task<IEnumerable<TEntity>> List();

        Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> predicate);

        Task Insert(TEntity entity);

        Task Update(TEntity entity);

        Task Update(IEnumerable<TEntity> entities);

        Task Delete(T id);      
    }
}
