using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectManagement.Domain.Tests.Entities
{
    [TestCategory("Domain")]
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void Should_ThrowException_When_Analyst_FirstName_IsEmpty()
        {
            Action action = () => new Analyst(string.Empty, "not empty");

            action.ShouldThrow<ArgumentException>().Which.ParamName.Contains("FirstName");
        }

        [TestMethod]
        public void Should_ThrowException_When_Analyst_LastName_IsEmpty()
        {
            Action action = () => new Analyst("not empty", string.Empty);

            action.ShouldThrow<ArgumentException>().Which.ParamName.Contains("LastName");
        }

        [TestMethod]
        public void Should_ThrowException_When_Consultant_FirstName_IsEmpty()
        {
            Action action = () => new Consultant(string.Empty, "not empty");

            action.ShouldThrow<ArgumentException>().Which.ParamName.Contains("FirstName");
        }

        [TestMethod]
        public void Should_ThrowException_When_Consultant_LastName_IsEmpty()
        {
            Action action = () => new Consultant("not empty", string.Empty);

            action.ShouldThrow<ArgumentException>().Which.ParamName.Contains("LastName");
        }

        [TestMethod]
        public void Should_Assign_Names_ToAnalyst()
        {
            var expected = new Analyst("firstName", "lastName");
            expected.FirstName.Should().Be("firstName");
            expected.LastName.Should().Be("lastName");
        }

        [TestMethod]
        public void Should_Assign_Names_ToConsultant()
        {
            var expected = new Consultant("firstName", "lastName");
            expected.FirstName.Should().Be("firstName");
            expected.LastName.Should().Be("lastName");
        }

        [TestMethod]
        public void Should_Generate_NewId_ToConsultant()
        {
            var analyst = new Consultant("firstName", "lastName");
            analyst.Id.Should().NotBe(Guid.Empty);
        }

        [TestMethod]
        public void Should_Generate_NewId_ToAnalyst()
        {
            var expected = new Analyst("firstName", "lastName");
            expected.Id.Should().NotBe(Guid.Empty);
        }
    }
}
