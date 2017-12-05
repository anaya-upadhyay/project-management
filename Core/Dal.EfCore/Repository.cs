using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Dal.EfCore
{
    /// <summary>
    /// A Repository using the underlying Entity Framework Core
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The underlying Entity Framework Core session
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

        /// <summary>
        /// Add an Entity to the Entity Framework Core Session
        /// </summary>
        /// <param name="entity">The Entity to be added</param>
        public void Add(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete an Entity from the Entity Framework Core Session
        /// </summary>
        /// <param name="entity">The Entity to be deleted</param>
        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Return an IQueryable of TEntity
        /// </summary>
        /// <returns>Return an IQueryable of TEntity</returns>
        public IQueryable<TEntity> Get()
        {
            return context.Set<TEntity>().AsQueryable();
        }
    }
}