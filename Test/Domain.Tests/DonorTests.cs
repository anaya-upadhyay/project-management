using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectManagement.Domain.Tests
{
    [TestCategory("Domain")]
    [TestClass]
    public class DonorTests
    {
        private Donor expectedDonor;

        [TestInitialize]
        public void Initialize()
        {
            expectedDonor = new Donor("name");
        }

        [TestMethod]
        public void Should_ThrowException_When_Name_IsEmpty()
        {
            Action expected = () => new Donor(string.Empty);

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