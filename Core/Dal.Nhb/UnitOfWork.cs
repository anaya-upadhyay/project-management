using NHibernate;

namespace ProjectManagement.Dal.Nhb
{
    /// <summary>
    ///     A Unit of Work conigured to work with NHibernate OR/M
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     The underlying context
        /// </summary>
        private readonly ISession session;

        /// <summary>
        ///     Create a new Unit of Work by passing an Active NHibernate session
        /// </summary>
        /// <param name="session">The Active NHibernate Session</param>
        public UnitOfWork(ISession session)
        {
            this.session = session;
        }

        /// <summary>
        /// Initialize a new NHibernate Transaction
        /// </summary>
        /// <returns>Returns a generic ITransaction implementation</returns>
        public ITransaction BeginTransaction()
        {
            return new Transaction(session.BeginTransaction());
        }

        /// <summary>
        /// Create a new Repository with the Active database context
        /// </summary>
        /// <returns>Returns an instance of an IRepository for the TEntity type</returns>
        public IRepository CreateRepository()
        {
            return new Repository(session);
        }

        /// <summary>
        /// Dispose the current Unit of Work and the underlying NHibernate session
        /// </summary>
        public void Dispose()
        {
            session.Dispose();
        }
    }
}