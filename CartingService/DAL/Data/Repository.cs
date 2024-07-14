using DAL.Common;
using DAL.Common.Interface;
using LiteDB;

namespace Infrastructure.Data;

public class Repository<TEntity, T> : IRepository<TEntity, T> where TEntity : BaseEntity<T>
{
    private readonly ILiteDatabaseConfiguration _config;

    public Repository(ILiteDatabaseConfiguration liteDatabaseConfiguration)
    {
        _config = liteDatabaseConfiguration;
    }

    public async Task<TEntity> GetById(T id)
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            var col = db.GetCollection<TEntity>();       
            return col.FindById(id.ToString());
        }
    }

    public async Task<IEnumerable<TEntity>> List()
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            return db.GetCollection<TEntity>().FindAll().ToList();
        }

    }

    public async Task<IEnumerable<TEntity>> List(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            return db.GetCollection<TEntity>().Find(predicate).ToList();
        }
    }

    public async Task Insert(TEntity entity)
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            var col = db.GetCollection<TEntity>();
            col.Insert(entity.Id.ToString(), entity);
        }
    }

    public async Task Update(TEntity entity)
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            var col = db.GetCollection<TEntity>();
            col.Update(entity.Id.ToString(),entity);
        }
    }

    public async Task Update(IEnumerable<TEntity> entities)
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            var col = db.GetCollection<TEntity>();
            col.Update(entities);
        }
    }

    public async Task Delete(T id)
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            var col = db.GetCollection<TEntity>();
            col.Delete(id.ToString());
        }
    }
}
