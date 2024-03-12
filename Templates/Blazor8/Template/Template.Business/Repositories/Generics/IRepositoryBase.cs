using System.Linq.Expressions;
using Template.Domain.Entities.Generics;

namespace Template.Business.Repositories.Generics
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        TEntity? GetById(object id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Save();
    }
}
