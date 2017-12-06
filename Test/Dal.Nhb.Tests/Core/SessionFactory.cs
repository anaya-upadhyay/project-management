using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using ProjectManagement.Dal.Nhb.Interceptors;
using ProjectManagement.Dal.Nhb.Listeners;

namespace ProjectManagement.Dal.Nhb.Tests.Core
{
    public sealed class SessionFactory
    {
        public static ISessionFactory GetSessionFactory()
        {
            return Fluently
                .Configure()
                .Database(
                    MsSqlConfiguration
                        .MsSql2012
                        .ConnectionString(conn => conn.FromConnectionStringWithKey("connection")))
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssemblyOf<UnitOfWork>()
                        .Conventions.Add(DefaultLazy.Never()))
                .ExposeConfiguration(cfg =>
                {
                    cfg.SetInterceptor(new SqlStatementInterceptor());
                    cfg.EventListeners.PreDeleteEventListeners = new IPreDeleteEventListener[] { new SoftDeletableListener() };
                    new SchemaExport(cfg).Create(false, true); // generate the DB schema but do not trace
                })
                .BuildSessionFactory();
        }
    }
}