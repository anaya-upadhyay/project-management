using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Dal.Nhb.Tests.Core;
using ProjectManagement.Domain;

namespace ProjectManagement.Dal.Nhb.Tests
{
    [TestClass]
    public class TransactionTests : BaseTest
    {
        private DonorAggregate donor;

        [TestInitialize]
        public void Initialize()
        {
            donor = new DonorAggregate("name");
        }

        [TestMethod]
        public void Should_NotSave_When_Rollback()
        {
            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (ITransaction tx = uow.BeginTransaction())
                {
                    var repo = uow.CreateRepository<DonorAggregate>();
                    repo.Add(donor);
                    tx.Rollback();
                }
            }
            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository<DonorAggregate>();
                var expected = repo.Get().FirstOrDefault(x => x.Id == donor.Id);
                expected.Should().BeNull();
            }
        }

        [TestMethod]
        public void Should_Save_When_Commit()
        {
            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (ITransaction tx = uow.BeginTransaction())
                {
                    var repo = uow.CreateRepository<DonorAggregate>();
                    repo.Add(donor);
                    tx.Commit();
                }
            }
            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository<DonorAggregate>();
                var expected = repo.Get().FirstOrDefault(x => x.Id == donor.Id);
                expected.Should().NotBeNull();
            }
        }
    }
}