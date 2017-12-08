using System;
using System.Linq;
using System.Transactions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectManagement.Api.Commands;
using ProjectManagement.Dal;
using ProjectManagement.Domain;

namespace ProjectManagement.Api.Handlers.Tests
{
    [TestClass]
    public class CreateProjectHandlerTests
    {
        private CreateProjectCommand fakeCommand;
        private Analyst fakeAnalyst;
        private DonorAggregate fakeDonor;
        private ProjectAggregate fakeProjectAggregate;

        private Mock<IUnitOfWork> mockUnitOfWork;
        private Mock<ITransaction> mockTransaction;
        private Mock<IRepository> mockRepository;

        [TestInitialize]
        public void Initialize()
        {
            // prepare the fakes
            fakeAnalyst = new Analyst("fake", "fake");
            fakeDonor = new DonorAggregate("fake");
            fakeCommand = new CreateProjectCommand(fakeDonor.Id, fakeAnalyst.Id, "acronym", "fake", "fake", DateTime.UtcNow);

            // initialize the mocks behaviour
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockTransaction = new Mock<ITransaction>();
            mockRepository = new Mock<IRepository>();

            mockRepository
                .Setup(x => x.Get<Analyst>())
                .Returns(new[] { fakeAnalyst }.AsQueryable());
            mockRepository
                .Setup(x => x.Get<DonorAggregate>())
                .Returns(new[] { fakeDonor }.AsQueryable());
            mockRepository
                .Setup(x => x.Add(It.IsAny<ProjectAggregate>()))
                .Callback<ProjectAggregate>((p) => fakeProjectAggregate = p);

            mockUnitOfWork
                .Setup(x => x.CreateRepository())
                .Returns(mockRepository.Object);
            mockUnitOfWork
                .Setup(x => x.BeginTransaction())
                .Returns(mockTransaction.Object);
        }


        [TestMethod]
        public void Should_Retrieve_Analyst()
        {
            var expectedHandler = new CreateProjectHandler(mockUnitOfWork.Object);

            expectedHandler.Execute(fakeCommand);

            mockRepository
                .Verify(x => x.Get<Analyst>(), Times.Once());
        }

        [TestMethod]
        public void Should_Retrieve_Donor()
        {
            var expectedHandler = new CreateProjectHandler(mockUnitOfWork.Object);

            expectedHandler.Execute(fakeCommand);

            mockRepository
                .Verify(x => x.Get<DonorAggregate>(), Times.Once());
        }

        [TestMethod]
        public void Should_AssignDefault_If_TypeOfProject_IsNotValid()
        {
            var expectedHandler = new CreateProjectHandler(mockUnitOfWork.Object);

            expectedHandler.Execute(fakeCommand);

            fakeProjectAggregate.ProjectType.Should().Be(TypeOfProject.None);
        }

        [TestMethod]
        public void Should_AssignCorrect_If_TypeOfProject_IsValid()
        {
            var expectedHandler = new CreateProjectHandler(mockUnitOfWork.Object);
            fakeCommand = new CreateProjectCommand(fakeDonor.Id, fakeAnalyst.Id, "acronym", "TaPackage", "fake", DateTime.UtcNow);

            expectedHandler.Execute(fakeCommand);

            fakeProjectAggregate.ProjectType.Should().Be(TypeOfProject.TaPackage);
        }

        [TestMethod]
        public void Should_AssignDefault_If_TypeOfTender_IsNotValid()
        {
            var expectedHandler = new CreateProjectHandler(mockUnitOfWork.Object);

            expectedHandler.Execute(fakeCommand);

            fakeProjectAggregate.TenderProcessType.Should().Be(TypeOfTenderProcess.None);
        }

        [TestMethod]
        public void Should_AssignCorrect_If_TypeOfTender_IsValid()
        {
            var expectedHandler = new CreateProjectHandler(mockUnitOfWork.Object);
            fakeCommand = new CreateProjectCommand(fakeDonor.Id, fakeAnalyst.Id, "acronym", "fake", "NegotiatedProcedure", DateTime.UtcNow);

            expectedHandler.Execute(fakeCommand);

            fakeProjectAggregate.TenderProcessType.Should().Be(TypeOfTenderProcess.NegotiatedProcedure);
        }

        [TestMethod]
        public void Should_Return_ValidMessage_When_Succeed()
        {
            var expectedHandler = new CreateProjectHandler(mockUnitOfWork.Object);

            var result = expectedHandler.Execute(fakeCommand);

            result.Success.Should().BeTrue();
            result.Reason.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void Should_Return_ValidMessage_When_Fails()
        {
            // throw exception
            mockUnitOfWork.Setup(x => x.CreateRepository())
                .Throws<NotImplementedException>();

            var expectedHandler = new CreateProjectHandler(mockUnitOfWork.Object);

            var result = expectedHandler.Execute(fakeCommand);

            result.Success.Should().BeFalse();
            result.Reason.Should().NotBeNullOrEmpty();
        }
    }
}