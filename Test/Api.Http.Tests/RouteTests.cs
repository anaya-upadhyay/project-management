using System.Web.Routing;
using FluentAssertions.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectManagement.Api.Http.Tests
{
    [TestClass]
    public class RouteTests
    {
        private RouteCollection routes;

        [TestInitialize]
        public void Initialize()
        {
            routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
        }

        [TestMethod]
        public void Should_Map_IndexPage()
        {
            var routeData = routes.GetRouteDataForUrl("/");

            routeData.Should()
                .HaveController("Home")
                .HaveAction("Index");
        }
    }
}