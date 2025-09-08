using MenteSaudavelAPI._02.Services.Interfaces.Repositories;
using System.Linq;
using System.Linq.Expressions;

namespace MenteSaudavelAPI._02.Services.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataBaseContext _context;

        public GenericRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async virtual Task<TEntity?> GetById(Guid id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public virtual void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }
    }
}