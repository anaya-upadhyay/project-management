using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

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
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();
        }
    }
}