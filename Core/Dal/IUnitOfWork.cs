using System;

namespace ProjectManagement.Dal
{
    /// <summary>
    ///     A Unit of Work capable of persisting objects
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Initialize a new Transaction
        /// </summary>
        /// <returns>Returns the newly initialized transaction</returns>
        ITransaction BeginTransaction();

        /// <summary>
        ///     Create a new Instance of a repository according to the TEntity type provided
        /// </summary>
        /// <typeparam name="TEntity">The Type of Entity to be used in this repository</typeparam>
        /// <returns>Returns an Active Repository for the Entity of type TEntity</returns>
        IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;
    }
}