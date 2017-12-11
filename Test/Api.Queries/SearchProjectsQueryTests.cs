using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Api.Queries;

namespace ProjectsManagement.Api.Queries.Tests
{
    [TestCategory("Api.Queries")]
    [TestClass]
    public class SearchProjectsQueryTests

    {
        [TestMethod]
        public void Should_hydrate_properties()
        {
            var query = new SearchProjectsQuery("some text", 1, 100);

            query.Text.Should().Be("some text");
            query.Page.Should().Be(1);
            query.Records.Should().Be(100);

        }
    }
}
