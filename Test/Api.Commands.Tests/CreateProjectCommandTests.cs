using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectManagement.Api.Commands.Tests
{
    [TestCategory("Api.Commands")]
    [TestClass]
    public class CreateProjectCommandTests
    {
        [TestMethod]
        public void Should_Hydrate_Properties()
        {
            var expectedGuid = Guid.NewGuid();
            var expectedDate = DateTime.UtcNow;
            var expected = new CreateProjectCommand(expectedGuid, expectedGuid, "acronym", "project type", "tender type", expectedDate);

            expected.AnalystId.Should().Be(expectedGuid);
            expected.DonorId.Should().Be(expectedGuid);
            expected.Acronym.Should().Be("acronym");
            expected.ProjectType.Should().Be("project type");
            expected.TenderProcessType.Should().Be("tender type");
            expected.StartDate.Should().Be(expectedDate);
        }
    }
}
