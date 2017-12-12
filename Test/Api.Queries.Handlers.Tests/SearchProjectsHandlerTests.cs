using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectManagement.Dal;
using ProjectManagement.Domain;

namespace ProjectManagement.Api.Queries.Handlers.Tests
{
    [TestCategory("Api.Queries.Handlers")]
    [TestClass]
    public class SearchProjectsHandlerTests
    {
        private readonly Analyst fakeAnaylist = new Analyst("first name", "last name", "acronym");

        private readonly DonorAggregate fakeDonor = new DonorAggregate("donor");
        private readonly SearchProjectsQuery fakeQuery = new SearchProjectsQuery("acronym", 1, 2);
        private IQueryable<ProjectAggregate> emptyFakeResult;

        private IQueryable<ProjectAggregate> fakeResult;
        private Mock<IRepository> mockRepository;
        private Mock<IUnitOfWork> mockUnitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockRepository = new Mock<IRepository>();
            fakeResult = new[]
            {
                new ProjectAggregate(fakeDonor, fakeAnaylist, "acronym", TypeOfProject.None, TypeOfTenderProcess.None,
                    DateTime.UtcNow),
                new ProjectAggregate(fakeDonor, fakeAnaylist, "acronym", TypeOfProject.None, TypeOfTenderProcess.None,
                    DateTime.UtcNow),
                new ProjectAggregate(fakeDonor, fakeAnaylist, "acronym", TypeOfProject.None, TypeOfTenderProcess.None,
                    DateTime.UtcNow),
                new ProjectAggregate(fakeDonor, fakeAnaylist, "acronym", TypeOfProject.None, TypeOfTenderProcess.None,
                    DateTime.UtcNow)
            }.AsQueryable();
            emptyFakeResult = new ProjectAggregate[0].AsQueryable();
        }

        [TestMethod]
        public void Should_ReturnsZero_When_NoRecordsAvailable()
        {
            mockRepository.Setup(x => x.Get<ProjectAggregate>())
                .Returns(emptyFakeResult);
            mockUnitOfWork
                .Setup(x => x.CreateRepository())
                .Returns(mockRepository.Object);
            var handler = new SearchProjectsHandler(mockUnitOfWork.Object);

            var result = handler.Handle(fakeQuery);

            result.Page.Should().Be(1);
            result.TotalRecords.Should().Be(0);
            result.Items.Should().HaveCount(0);
        }

        [TestMethod]
        public void Should_ReturnsCount_When_RecordsAvailable()
        {
            mockRepository.Setup(x => x.Get<ProjectAggregate>())
                .Returns(fakeResult);
            mockUnitOfWork
                .Setup(x => x.CreateRepository())
                .Returns(mockRepository.Object);
            var handler = new SearchProjectsHandler(mockUnitOfWork.Object);

            var result = handler.Handle(fakeQuery);

            result.Page.Should().Be(1);
            result.TotalRecords.Should().Be(4);
        }

        [TestMethod]
        public void Should_ReturnsRecords_When_RecordsAvailable()
        {
            mockRepository.Setup(x => x.Get<ProjectAggregate>())
                .Returns(fakeResult);
            mockUnitOfWork
                .Setup(x => x.CreateRepository())
                .Returns(mockRepository.Object);
            var handler = new SearchProjectsHandler(mockUnitOfWork.Object);

            var result = handler.Handle(fakeQuery);

            result.Items.Should().HaveCount(2);
        }

        [TestMethod]
        public void Should_MapResultsCorrectly_When_RecordsAvailable()
        {
            mockRepository.Setup(x => x.Get<ProjectAggregate>())
                .Returns(fakeResult);
            mockUnitOfWork
                .Setup(x => x.CreateRepository())
                .Returns(mockRepository.Object);
            var handler = new SearchProjectsHandler(mockUnitOfWork.Object);

            var result = handler.Handle(fakeQuery);
            var item = result.Items[0];

            result.Items[0].Id.Should().NotBeEmpty();
            result.Items[0].Acronym.Should().Be("acronym");
            result.Items[0].Analyst.Should().Be("last name, first name (acronym)");
            result.Items[0].Status.Should().Be("InPreparation");
            result.Items[0].TierLevel.Should().Be("Tier1");


        }
    }
}