using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Dal.Nhb.Tests.Core;
using ProjectManagement.Domain;

namespace ProjectManagement.Dal.Nhb.Tests.Mappings
{
    [TestCategory("Dal.Nhb")]
    [TestClass]
    public class ProjectMappingTests : BaseTest
    {
        [TestMethod]
        public void Should_Save_New_Project()
        {
            var donor = new Donor("My Donor");
            var project = new ProjectAggregate(donor, "My Project", ProjectType.TaPackage);

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository<Donor>())
                    {
                        repo.Add(donor);
                    }
                    using (var repo = uow.CreateRepository<ProjectAggregate>())
                    {
                        repo.Add(project);
                    }
                    tx.Commit();
                }
            }

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository<ProjectAggregate>();
                var expected = repo.Get().Single(x => x.Id == project.Id);

                expected.Should().NotBeNull();
            }
        }
    }
}