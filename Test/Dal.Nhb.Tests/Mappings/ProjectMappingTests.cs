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
            var donor = new DonorAggregate("My Donor");
            var analyst = new Analyst("firstName", "lastName");
            var project = new ProjectAggregate(donor, analyst, "My Project", TypeOfProject.TaPackage, TypeOfTenderProcess.NegotiatedProcedure);

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository<DonorAggregate>())
                    {
                        repo.Add(donor);
                    }
                    using (var repo = uow.CreateRepository<Person>())
                    {
                        repo.Add(analyst);
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

        [TestMethod]
        public void Should_SoftDelete_A_Project_When_RemovedWithARepository()
        {
            var donor = new DonorAggregate("My Donor");
            var analyst = new Analyst("firstName", "lastName");
            var project = new ProjectAggregate(donor, analyst, "My Project", TypeOfProject.TaPackage, TypeOfTenderProcess.NegotiatedProcedure);

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository<DonorAggregate>())
                    {
                        repo.Add(donor);
                    }
                    using (var repo = uow.CreateRepository<Person>())
                    {
                        repo.Add(analyst);
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
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository<ProjectAggregate>())
                    {
                        repo.Delete(project);
                    }
                    tx.Commit();
                }
            }

            // force NHibernate to select a soft deleted object
            var session = GetSession();
            var expected = (ProjectAggregate)session
                .CreateSQLQuery($"select proj.* from Projects proj where Id = '{project.Id}'")
                .AddEntity("proj", typeof(ProjectAggregate))
                .UniqueResult();
            expected.Should().NotBeNull();
            expected.IsDeleted.Should().BeTrue();
        }
    }
}