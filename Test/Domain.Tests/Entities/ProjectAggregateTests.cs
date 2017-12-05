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
        private Donor fakeDonor;

        [TestInitialize]
        public void Initialize()
        {
            fakeDonor = new Donor("some name");
            expectedProject = new ProjectAggregate(fakeDonor, "acronym");
        }

        [TestMethod]
        public void Should_ThrowException_When_Acronym_IsEmpty()
        {
            Action expected = () => new ProjectAggregate(fakeDonor, string.Empty);

            expected.ShouldThrow<ArgumentException>().Which.ParamName.Contains("Donor");
        }

        [TestMethod]
        public void Should_ThrowException_When_Donor_IsNull()
        {
            Action expected = () => new ProjectAggregate(null, "acronym");

            expected.ShouldThrow<ArgumentException>().Which.ParamName.Contains("Acronym");
        }

        [TestMethod]
        public void Should_Assign_Acronym()
        {
            expectedProject.Acronym.Should().Be("acronym");
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
        public void Should_RaiseEvent_When_Created()
        {
            ProjectAggregate expected = null;
            DomainEvents.Register<ProjectCreated>(p => expected = p.Project);

            var project = new ProjectAggregate(fakeDonor, "acronym");

            expected.Should().NotBeNull();
            expected.Should().Be(project);
        }
    }
}