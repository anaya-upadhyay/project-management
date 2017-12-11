using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectManagement.Api.Http.Tests
{
    [TestCategory("Api.Http")]
    [TestClass]
    public class FilterTests
    {
        private GlobalFilterCollection filters;

        [TestInitialize]
        public void Initialize()
        {
            filters = new GlobalFilterCollection();
            FilterConfig.RegisterGlobalFilters(filters);
        }

        [TestMethod]
        public void Should_Contain_GlobalErrorHandler()
        {
            filters.Should().ContainSingle()
                .Which.Instance.Should().BeOfType<HandleErrorAttribute>();
        }
    }
}
