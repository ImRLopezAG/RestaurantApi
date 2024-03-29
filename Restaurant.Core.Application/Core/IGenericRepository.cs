using System.Linq.Expressions;

namespace Restaurant.Core.Application.Core;

public interface IGenericRepository<TEntity> where TEntity : class {
  Task<IEnumerable<TEntity>> GetAll();
  Task<TEntity> GetEntity(int id);
  Task<bool> Exists(Expression<Func<TEntity, bool>> Filter);
  Task<TEntity> Save(TEntity entity);
  Task<TEntity> Update(TEntity entity);

  Task Delete(TEntity entity);
}