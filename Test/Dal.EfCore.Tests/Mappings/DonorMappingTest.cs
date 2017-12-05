using System.Linq;
using Dal.EfCore.Tests.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Dal;
using ProjectManagement.Dal.EfCore;
using ProjectManagement.Domain;

namespace Dal.EfCore.Tests.Mappings
{
    [TestClass]
    public class DonorMappingTest : BaseTest
    {
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
    }
}