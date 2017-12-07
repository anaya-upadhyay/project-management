using System.Linq;
using NHibernate;

namespace ProjectManagement.Dal.Nhb
{
    /// <summary>
    /// A Repository using the underlying NHibernate
    /// </summary>
    public sealed class Repository : IRepository
    {
        /// <summary>
        /// The underlying NHibernate session
        /// </summary>
        private readonly ISession session;

        public Repository(ISession session)
        {
            this.session = session;
        }

        public void Dispose()
        {
            // do nothing because the parent uow should dispose the session
        }

        /// <inheritdoc />
        /// <summary>
        /// Add an Entity to the NHibernate Session
        /// </summary>
        /// <param name="entity">The Entity to be added</param>
        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            session.Save(entity);
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete an Entity from the NHibernate Session
        /// </summary>
        /// <param name="entity">The Entity to be deleted</param>
        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            session.Delete(entity);
        }

        /// <inheritdoc />
        /// <summary>
        /// Return an IQueryable of TEntity
        /// </summary>
        /// <returns>Return an IQueryable of TEntity</returns>
        public IQueryable<TEntity> Get<TEntity>() where TEntity : class
        {
            return session.Query<TEntity>();
        }
    }
}