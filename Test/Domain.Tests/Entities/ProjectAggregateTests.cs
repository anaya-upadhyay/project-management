using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Domain.Events;

namespace ProjectManagement.Domain.Tests.Entities
{
    [TestCategory("Domain")]
    [TestClass]
    public class ProjectAggregateTests
    {
        private ProjectAggregate expectedProject;
        private DonorAggregate fakeDonor;
        private Analyst fakeAnalyst;

        [TestInitialize]
        public void Initialize()
        {
            fakeDonor = new DonorAggregate("some name");
            fakeAnalyst = new Analyst("firstName", "lastName");
            expectedProject = new ProjectAggregate(fakeDonor, fakeAnalyst, "acronym", TypeOfProject.TaPackage, TypeOfTenderProcess.NegotiatedProcedure);
        }

        [TestMethod]
        public void Should_ThrowException_When_Acronym_IsEmpty()
        {
            Action expected = () => new ProjectAggregate(fakeDonor, fakeAnalyst, string.Empty, TypeOfProject.TaPackage, TypeOfTenderProcess.NegotiatedProcedure);

            expected.ShouldThrow<ArgumentException>().Which.ParamName.Contains("Donor");
        }

        [TestMethod]
        public void Should_ThrowException_When_Donor_IsNull()
        {
            Action expected = () => new ProjectAggregate(null, fakeAnalyst, "acronym", TypeOfProject.TaPackage, TypeOfTenderProcess.NegotiatedProcedure);

            expected.ShouldThrow<ArgumentException>().Which.ParamName.Contains("Donor");
        }

        [TestMethod]
        public void Should_ThrowException_When_Analyst_IsNull()
        {
            Action expected = () => new ProjectAggregate(fakeDonor, null, "acronym", TypeOfProject.TaPackage, TypeOfTenderProcess.NegotiatedProcedure);

            expected.ShouldThrow<ArgumentException>().Which.ParamName.Contains("Analyst");
        }

        [TestMethod]
        public void Should_Assign_Acronym()
        {
            expectedProject.Acronym.Should().Be("acronym");
        }

        [TestMethod]
        public void Should_Assign_TypeOfProject()
        {
            expectedProject.ProjectType.Should().Be(TypeOfProject.TaPackage);
        }

        [TestMethod]
        public void Should_Assign_TypeOfTenderProcess()
        {
            expectedProject.TenderProcessType.Should().Be(TypeOfTenderProcess.NegotiatedProcedure);
        }

        [TestMethod]
        public void Should_Assign_Analyst()
        {
            expectedProject.Analyst.Should().Be(fakeAnalyst);
        }

        [TestMethod]
        public void Should_Generate_NewId()
        {
            expectedProject.Id.Should().NotBe(Guid.Empty);
        }

        [TestMethod]
        public void Should_Assign_Donor()
        {
            expectedProject.Donor.Should().Be(fakeDonor);
        }

        [TestMethod]
        public void Should_BeAdded_ToDonor_Projects_WhenCreated()
        {
            fakeDonor.Projects.Should().Contain(expectedProject);
        }

        [TestMethod]
        public void Should_Assign_StartDate_When_Provided()
        {
            DateTime startDate = DateTime.UtcNow;
            var expected = new ProjectAggregate(fakeDonor, fakeAnalyst, "acronym", TypeOfProject.TaPackage, TypeOfTenderProcess.NegotiatedProcedure, startDate);

            expected.ExpectedStartDate.Should().Be(startDate);
        }

        [TestMethod]
        public void Should_Assign_UtcNow_When_NotProvided()
        {
            DateTime startDate = DateTime.UtcNow;
            var expected = new ProjectAggregate(fakeDonor, fakeAnalyst, "acronym", TypeOfProject.TaPackage, TypeOfTenderProcess.NegotiatedProcedure, startDate);

            expected.ExpectedStartDate.Should().BeOnOrAfter(startDate);
        }

        [TestMethod]
        public void Should_RaiseEvent_When_Created()
        {
            ProjectAggregate expected = null;
            DomainEvents.Register<ProjectCreated>(p => expected = p.Project);

            var project = new ProjectAggregate(fakeDonor, fakeAnalyst, "acronym", TypeOfProject.TaPackage, TypeOfTenderProcess.NegotiatedProcedure);

            expected.Should().NotBeNull();
            expected.Should().Be(project);
        }

    }
}