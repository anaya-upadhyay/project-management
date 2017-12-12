using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectManagement.Api.Buses.Commands;
using ProjectManagement.Api.Commands;
using ProjectManagement.Api.Http.Controllers;
using System.Web.Http.Results;
using ProjectManagement.Api.Handlers.Core;
using ProjectManagement.Api.Queries;
using ProjectManagement.Api.Queries.Core;
using ProjectManagement.Api.Queries.Results;

namespace ProjectManagement.Api.Http.Tests.Controllers
{
    [TestCategory("Api.Http")]
    [TestClass]
    public class ProjectsControllerTests
    {
        private Mock<IBus> mockBus;
        private CreateProjectCommand fakeCommand;
        private SearchProjectsQuery fakeQuery;

        [TestInitialize]
        public void Initialize()
        {
            mockBus = new Mock<IBus>();
            fakeCommand = new CreateProjectCommand(Guid.Empty, Guid.Empty, "fake","fake", "fake", null);
            fakeQuery = new SearchProjectsQuery("text", 1, 100);
        }

        [TestMethod]
        public void Should_Return400_When_Command_IsNotValid()
        {
            mockBus.Setup(x => x.Validate(fakeCommand))
                .Returns(new[] { new ValidationResult("key", "error message") });

            var controller = new ProjectsController(mockBus.Object);
            var result = controller.Post(fakeCommand);

            result
                .Should().BeOfType<InvalidModelStateResult>();

        }

        [TestMethod]
        public void Should_ContainsErrors_When_CommandIsInvalid()
        {
            mockBus.Setup(x => x.Validate(fakeCommand))
                .Returns(new[] { new ValidationResult("key", "error message") });

            var controller = new ProjectsController(mockBus.Object);
            var result = controller.Post(fakeCommand);

            result
                .Should().BeOfType<InvalidModelStateResult>()
                .Which.ModelState.ContainsKey("key").Should().BeTrue();
        }


        [TestMethod]
        public void Should_Return200_When_Command_IsValid()
        {
            mockBus.Setup(x => x.Validate(fakeCommand))
                .Returns<IEnumerable<ValidationResult>>(null);

            var controller = new ProjectsController(mockBus.Object);
            var result = controller.Post(fakeCommand);

            result
                .Should().BeOfType<OkNegotiatedContentResult<ICommandResult>>();
        }

        [TestMethod]
        public void Should_Return200_When_Command_Execute()
        {
            mockBus.Setup(x => x.Validate(fakeCommand))
                .Returns<IEnumerable<ValidationResult>>(null);
            mockBus.Setup(x => x.Submit(fakeCommand))
                .Returns(new SimpleCommandResult(true));

            var controller = new ProjectsController(mockBus.Object);
            var result = controller.Post(fakeCommand);

            result
                .Should().BeOfType<OkNegotiatedContentResult<ICommandResult>>()
                .Which.Content.Success.Should().BeTrue();
        }

        [TestMethod]
        public void Should_Return200_When_Command_Fail()
        {
            mockBus.Setup(x => x.Validate(fakeCommand))
                .Returns<IEnumerable<ValidationResult>>(null);
            mockBus.Setup(x => x.Submit(fakeCommand))
                .Returns(new SimpleCommandResult(false, "An error occurred"));

            var controller = new ProjectsController(mockBus.Object);
            var result = controller.Post(fakeCommand);

            result
                .Should().BeOfType<OkNegotiatedContentResult<ICommandResult>>()
                .Which.Content.Success.Should().BeFalse();
            result
                .Should().BeOfType<OkNegotiatedContentResult<ICommandResult>>()
                .Which.Content.Reason.Should().Be("An error occurred");
        }

        [TestMethod]
        public void Should_Return200_When_Execute_Query()
        {
            mockBus.Setup(x => x.Query<SearchProjectsQuery, IPaginatedResult<ProjectItemResult>>(fakeQuery))
                .Returns(new PaginatedResult<ProjectItemResult>(1, 100, new ProjectItemResult[0]));

            var controller = new ProjectsController(mockBus.Object);
            var result = controller.Get(fakeQuery);

            result
                .Should().BeOfType<OkNegotiatedContentResult<IPaginatedResult<ProjectItemResult>>>()
                .Which.Content.Should().NotBeNull();
        }
    }
}