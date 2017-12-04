using System;
using System.Linq;

namespace ProjectManagement.Dal
{
    /// <summary>
    ///     Represents a Generic Repository Contract
    /// </summary>
    /// <typeparam name="TEntity">The Type of Entity exposed by the Repository</typeparam>
    public interface IRepository<TEntity> : IDisposable
    {
        /// <summary>
        ///     Add an entity to the collection of New/Updated entities
        /// </summary>
        /// <param name="entity">The Entity to be added</param>
        void Add(TEntity entity);

        /// <summary>
        ///     Delete an existing entity from the Data Store
        /// </summary>
        /// <param name="entity">The Entity to be deleted</param>
        void Delete(TEntity entity);

        /// <summary>
        ///     Get an IQueryable collection of a specific Entity
        /// </summary>
        /// <returns>Returns an IQueryable collection of objects</returns>
        IQueryable<TEntity> Get();
    }
}