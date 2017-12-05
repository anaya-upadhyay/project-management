using NHibernate;

namespace ProjectManagement.Dal.Nhb
{
    /// <summary>
    ///     A Unit of Work conigured to work with NHibernate OR/M
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ISession session;

        public UnitOfWork(ISession session)
        {
            this.session = session;
        }

        public ITransaction BeginTransaction()
        {
            return new Transaction(session.BeginTransaction());
        }

        public IRepository<TEntity> CreateRepository<TEntity>()
        {
            return new Repository<TEntity>(session);
        }

        public void Dispose()
        {
            session.Dispose();
        }
    }
}