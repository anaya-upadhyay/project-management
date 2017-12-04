using System.Linq;
using NHibernate;

namespace ProjectManagement.Dal.Nhb
{
    public sealed class Repository<TEntity> : IRepository<TEntity>
    {
        private readonly ISession session;

        public Repository(ISession session)
        {
            this.session = session;
        }

        public void Dispose()
        {
            // do nothing because the parent uow should dispose the session
        }

        public void Add(TEntity entity)
        {
            session.Save(entity);
        }

        public void Delete(TEntity entity)
        {
            session.Delete(entity);
        }

        public IQueryable<TEntity> Get()
        {
            return session.Query<TEntity>();
        }
    }
}