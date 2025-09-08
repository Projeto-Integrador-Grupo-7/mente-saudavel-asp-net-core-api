using System.Linq.Expressions;

namespace MenteSaudavelAPI._02.Services.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        void SaveChanges();

        IQueryable<TEntity> GetAll();

        Task<TEntity?> GetById(Guid id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        bool Any(Expression<Func<TEntity, bool>> predicate);
    }
}