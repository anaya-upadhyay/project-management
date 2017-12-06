using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectManagement.Domain.Tests.Entities
{
    [TestCategory("Domain")]
    [TestClass]
    public class DonorTests
    {
        private DonorAggregate expectedDonor;

        [TestInitialize]
        public void Initialize()
        {
            expectedDonor = new DonorAggregate("name");
        }

        [TestMethod]
        public void Should_ThrowException_When_Name_IsEmpty()
        {
            Action expected = () => new DonorAggregate(string.Empty);

            expected.ShouldThrow<ArgumentException>().Which.ParamName.Contains("Name");
        }

        [TestMethod]
        public void Should_Assign_Name()
        {
            expectedDonor.Name.Should().Be("name");
        }

        [TestMethod]
        public void Should_Initialize_Projects()
        {
            expectedDonor.Projects.Count.Should().Be(0);
        }

        [TestMethod]
        public void Should_Generate_NewId()
        {
            expectedDonor.Id.Should().NotBe(Guid.Empty);
        }
    }
}