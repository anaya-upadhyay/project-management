using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Dal.Nhb.Tests.Core;
using ProjectManagement.Domain;

namespace ProjectManagement.Dal.Nhb.Tests.Mappings
{
    [TestClass]
    public class PersonTests : BaseTest
    {
        [TestMethod]
        public void Should_Save_New_Analyst()
        {
            var analyst = new Analyst("firstName", "lastName");

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

        }

        [TestMethod]
        public void Should_Delete_Existing_Analyst()
        {

        }

        [TestMethod]
        public void Should_Delete_Existing_Consultant()
        {

        }
    }
}
