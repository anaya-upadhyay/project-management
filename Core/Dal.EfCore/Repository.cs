using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Dal.EfCore
{
    /// <summary>
    ///     A Repository using the underlying Entity Framework Core
    /// </summary>
    public sealed class Repository : IRepository
    {
        /// <summary>
        ///     The underlying Entity Framework Core session
        /// </summary>
        private readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            // do nothing because the parent uow should dispose the session
        }

        /// <inheritdoc />
        /// <summary>
        ///     Add an Entity to the Entity Framework Core Session
        /// </summary>
        /// <param name="entity">The Entity to be added</param>
        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Delete an Entity from the Entity Framework Core Session
        /// </summary>
        /// <param name="entity">The Entity to be deleted</param>
        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Return an IQueryable of TEntity
        /// </summary>
        /// <returns>Return an IQueryable of TEntity</returns>
        public IQueryable<TEntity> Get<TEntity>() where TEntity : class
        {
            return context.Set<TEntity>().AsQueryable();
        }
    }
}