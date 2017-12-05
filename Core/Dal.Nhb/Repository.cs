﻿using System.Linq;
using NHibernate;

namespace ProjectManagement.Dal.Nhb
{
    /// <summary>
    /// A Repository using the underlying NHibernate
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class Repository<TEntity> : IRepository<TEntity>
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

        /// <summary>
        /// Add an Entity to the NHibernate Session
        /// </summary>
        /// <param name="entity">The Entity to be added</param>
        public void Add(TEntity entity)
        {
            session.Save(entity);
        }

        /// <summary>
        /// Delete an Entity from the NHibernate Session
        /// </summary>
        /// <param name="entity">The Entity to be deleted</param>
        public void Delete(TEntity entity)
        {
            session.Delete(entity);
        }

        /// <summary>
        /// Return an IQueryable of TEntity
        /// </summary>
        /// <returns>Return an IQueryable of TEntity</returns>
        public IQueryable<TEntity> Get()
        {
            return session.Query<TEntity>();
        }
    }
}