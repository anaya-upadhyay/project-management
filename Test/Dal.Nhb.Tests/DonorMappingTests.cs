using System.Linq;
using FluentAssertions;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using ProjectManagement.Domain;

namespace ProjectManagement.Dal.Nhb.Tests
{
    [TestCategory("Dal.Nhb")]
    [TestClass]
    public class DonorMappingTests
    {
        private ISessionFactory sessionFactory;

        [TestInitialize]
        public void Initialize()
        {
            sessionFactory = Fluently
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
                    new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();
        }

        private ISession GetSession()
        {
            return sessionFactory.OpenSession();
        }

        [TestMethod]
        public void Should_Save_New_Donor()
        {
            var donor = new Donor("Donor Name");

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository<Donor>())
                    {
                        repo.Add(donor);
                        tx.Commit();
                    }
                }
            }

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository<Donor>();
                var expected = repo.Get().Single(x => x.Id == donor.Id);

                expected.Should().NotBeNull();
            }
        }

        [TestMethod]
        public void Should_Delete_Existing_Donor()
        {
            var donor = new Donor("Donor Name");

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository<Donor>())
                    {
                        repo.Add(donor);
                        tx.Commit();
                    }
                }
            }

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository<Donor>())
                    {
                        repo.Delete(donor);
                        tx.Commit();
                    }
                }
            }

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository<Donor>();
                var expected = repo.Get().FirstOrDefault(x => x.Id == donor.Id);

                expected.Should().BeNull();
            }
        }
    }
}