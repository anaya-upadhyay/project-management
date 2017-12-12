using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectManagement.Api.Handlers;
using ProjectManagement.Dal;
using ProjectManagement.Domain;

namespace ProjectManagement.Api.Commands.Handlers.Tests
{
    [TestCategory("Api.Commands.Handlers")]
    [TestClass]
    public class CanCreateProjectTests
    {
        private CreateProjectCommand fakeCommand;
        private Analyst fakeAnalyst;
        private DonorAggregate fakeDonor;

        private Mock<IUnitOfWork> mockUnitOfWork;
        private Mock<IRepository> mockRepository;

        [TestInitialize]
        public void Initialize()
        {
            // prepare the fakes
            fakeAnalyst = new Analyst("fake", "fake", "fake");
            fakeDonor = new DonorAggregate("fake");
            fakeCommand = new CreateProjectCommand(fakeDonor.Id, fakeAnalyst.Id, "acronym", "fake", "fake", DateTime.UtcNow);

            // initialize the mocks behaviour
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockRepository = new Mock<IRepository>();
        }

        [TestMethod]
        public void Should_ReturnError_When_Donor_DoesNotExist()
        {
            var validator = new CanCreateProject(mockUnitOfWork.Object);
            mockRepository
                .Setup(x => x.Get<Analyst>())
                .Returns(new[] { fakeAnalyst }.AsQueryable());
            mockUnitOfWork
                .Setup(x => x.CreateRepository())
                .Returns(mockRepository.Object);

            var results = validator.Validate(fakeCommand);

            results.Should().HaveCount(1)
                .And.ContainSingle(x => x.Name == "Donor");
        }

        [TestMethod]
        public void Should_ReturnError_When_Analyst_DoesNotExist()
        {
            var validator = new CanCreateProject(mockUnitOfWork.Object);
            mockRepository
                .Setup(x => x.Get<DonorAggregate>())
                .Returns(new[] { fakeDonor }.AsQueryable());
            mockUnitOfWork
                .Setup(x => x.CreateRepository())
                .Returns(mockRepository.Object);

            var results = validator.Validate(fakeCommand);

            results.Should().HaveCount(1)
                .And.ContainSingle(x => x.Name == "Analyst");
        }

        [TestMethod]
        public void Should_ReturnTwoErrors_When_Analyst_AndDonor_DoNotExist()
        {
            var validator = new CanCreateProject(mockUnitOfWork.Object);
            mockUnitOfWork
                .Setup(x => x.CreateRepository())
                .Returns(mockRepository.Object);

            var results = validator.Validate(fakeCommand);

            results.Should().HaveCount(2)
                .And.ContainSingle(x => x.Name == "Donor")
                .And.ContainSingle(x => x.Name == "Analyst");
        }

        [TestMethod]
        public void Should_NotReturnError_When_Analyst_AndDonor_Exist()
        {
            var validator = new CanCreateProject(mockUnitOfWork.Object);
            mockRepository
                .Setup(x => x.Get<Analyst>())
                .Returns(new[] { fakeAnalyst }.AsQueryable());
            mockRepository
                .Setup(x => x.Get<DonorAggregate>())
                .Returns(new[] { fakeDonor }.AsQueryable());
            mockUnitOfWork
                .Setup(x => x.CreateRepository())
                .Returns(mockRepository.Object);

            var results = validator.Validate(fakeCommand);

            results.Should().HaveCount(0);
        }
    }
}
