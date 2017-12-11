using FluentAssertions.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Api.Http.Controllers;

namespace ProjectManagement.Api.Http.Tests.Controllers
{
    [TestCategory("Api.Http")]
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Should_Set_Title_OnIndex()
        {
            var controller = new HomeController();

            var result = controller.Index();

            result.Should().BeViewResult()
                .WithViewName("Index");
        }
    }
}