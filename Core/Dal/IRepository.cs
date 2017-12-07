using System;
using System.Linq;

namespace ProjectManagement.Dal
{
    /// <summary>
    ///     Represents a Generic Repository Contract
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        ///     Add an entity to the collection of New/Updated entities
        /// </summary>
        /// <param name="entity">The Entity to be added</param>
        void Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        ///     Delete an existing entity from the Data Store
        /// </summary>
        /// <param name="entity">The Entity to be deleted</param>
        void Delete<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        ///     Get an IQueryable collection of a specific Entity
        /// </summary>
        /// <returns>Returns an IQueryable collection of objects</returns>
        IQueryable<TEntity> Get<TEntity>() where TEntity : class;
    }
}