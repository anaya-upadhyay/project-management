using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Dal.Nhb.Tests.Core;
using ProjectManagement.Domain;

namespace ProjectManagement.Dal.Nhb.Tests.Mappings
{
    [TestCategory("Dal.Nhb")]
    [TestClass]
    public class DonorMappingTests : BaseTest
    {
        [TestMethod]
        public void Should_Save_New_Donor()
        {
            var donor = new DonorAggregate("Donor Name");

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository())
                    {
                        repo.Add(donor);
                        tx.Commit();
                    }
                }
            }

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository();
                var expected = repo.Get<DonorAggregate>().Single(x => x.Id == donor.Id);

                expected.Should().NotBeNull();
            }
        }

        [TestMethod]
        public void Should_Delete_Existing_Donor()
        {
            var donor = new DonorAggregate("Donor Name");

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository())
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
                    using (var repo = uow.CreateRepository())
                    {
                        repo.Delete(donor);
                        tx.Commit();
                    }
                }
            }

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository();
                var expected = repo.Get<DonorAggregate>().FirstOrDefault(x => x.Id == donor.Id);

                expected.Should().BeNull();
            }
        }
    }
}