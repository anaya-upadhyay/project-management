using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Dal.EfCore
{
    /// <summary>
    ///     A Unit of Work conigured to work with Entity Framework Core OR/M
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     The underlying context
        /// </summary>
        private readonly DbContext context;

        /// <summary>
        ///     Create a new Unit of Work by passing an Active DbContext
        /// </summary>
        /// <param name="context">The Active Entity Framework Core DbContext</param>
        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        /// <summary>
        ///     Dispose the current Unit of Work and the underlying Entity Framework session
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }

        /// <summary>
        ///     Initialize a new Entity Framework Core Transaction
        /// </summary>
        /// <returns>Returns a generic ITransaction implementation</returns>
        public ITransaction BeginTransaction()
        {
            return new Transaction(context.Database.BeginTransaction());
        }

        /// <summary>
        ///     Create a new Repository with the Active database context
        /// </summary>
        /// <typeparam name="TEntity">The Entity to be managed by this repository</typeparam>
        /// <returns>Returns an instance of an IRepository for the TEntity type</returns>
        public IRepository CreateRepository()
        {
            return new Repository(context);
        }
    }
}