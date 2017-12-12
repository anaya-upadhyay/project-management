using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Dal.Nhb.Tests.Core;
using ProjectManagement.Domain;

namespace ProjectManagement.Dal.Nhb.Tests.Mappings
{
    [TestCategory("Domain")]
    [TestClass]
    public class PersonTests : BaseTest
    {
        [TestMethod]
        public void Should_Save_New_Analyst()
        {
            var analyst = new Analyst("firstName", "lastName", "acronym");

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository())
                    {
                        repo.Add(analyst);
                        tx.Commit();
                    }
                }
            }

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository();
                var expected = repo.Get<Analyst>().Single(x => x.Id == analyst.Id);

                expected.Should().NotBeNull();
            }
        }

        [TestMethod]
        public void Should_Save_New_Consultant()
        {
            var consultant = new Consultant("firstName", "lastName");

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository())
                    {
                        repo.Add(consultant);
                        tx.Commit();
                    }
                }
            }

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository();
                var expected = repo.Get<Consultant>().Single(x => x.Id == consultant.Id);

                expected.Should().NotBeNull();
            }
        }

        [TestMethod]
        public void Should_Delete_Existing_Analyst()
        {
            var analyst = new Analyst("firstName", "lastName", "acronym");

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository())
                    {
                        repo.Add(analyst);
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
                        repo.Delete(analyst);
                        tx.Commit();
                    }
                }
            }

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository();
                var expected = repo.Get<Analyst>().FirstOrDefault(x => x.Id == analyst.Id);

                expected.Should().BeNull();
            }
        }

        [TestMethod]
        public void Should_Delete_Existing_Consultant()
        {
            var consultant = new Consultant("firstName", "lastName");

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (var tx = uow.BeginTransaction())
                {
                    using (var repo = uow.CreateRepository())
                    {
                        repo.Add(consultant);
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
                        repo.Delete(consultant);
                        tx.Commit();
                    }
                }
            }

            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository();
                var expected = repo.Get<Consultant>().FirstOrDefault(x => x.Id == consultant.Id);

                expected.Should().BeNull();
            }
        }
    }
}
