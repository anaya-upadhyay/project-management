using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectManagement.Api.Buses.Commands;
using ProjectManagement.Api.Commands;
using ProjectManagement.Api.Http.Controllers;
using System.Web.Http.Results;
using ProjectManagement.Api.Handlers.Core;

namespace ProjectManagement.Api.Http.Tests.Controllers
{
    [TestClass]
    public class ProjectsControllerTests
    {
        private Mock<ICommandBus> mockCommandBus;
        private CreateProjectCommand fakeCommand;

        [TestInitialize]
        public void Initialize()
        {
            mockCommandBus = new Mock<ICommandBus>();
            fakeCommand = new CreateProjectCommand(Guid.Empty, Guid.Empty, "fake","fake", "fake", null);
        }

        [TestMethod]
        public void Should_Return400_When_Command_IsNotValid()
        {
            mockCommandBus.Setup(x => x.Validate(fakeCommand))
                .Returns(new[] { new ValidationResult("key", "error message") });

            var controller = new ProjectsController(mockCommandBus.Object);
            var result = controller.Post(fakeCommand);

            result
                .Should().BeOfType<InvalidModelStateResult>();

        }

        [TestMethod]
        public void Should_ContainsErrors_When_CommandIsInvalid()
        {
            mockCommandBus.Setup(x => x.Validate(fakeCommand))
                .Returns(new[] { new ValidationResult("key", "error message") });

            var controller = new ProjectsController(mockCommandBus.Object);
            var result = controller.Post(fakeCommand);

            result
                .Should().BeOfType<InvalidModelStateResult>()
                .Which.ModelState.ContainsKey("key").Should().BeTrue();
        }


        [TestMethod]
        public void Should_Return200_When_Command_IsValid()
        {
            mockCommandBus.Setup(x => x.Validate(fakeCommand))
                .Returns<IEnumerable<ValidationResult>>(null);

            var controller = new ProjectsController(mockCommandBus.Object);
            var result = controller.Post(fakeCommand);

            result
                .Should().BeOfType<OkNegotiatedContentResult<ICommandResult>>();
        }

        [TestMethod]
        public void Should_Return200_When_Command_Execute()
        {
            mockCommandBus.Setup(x => x.Validate(fakeCommand))
                .Returns<IEnumerable<ValidationResult>>(null);
            mockCommandBus.Setup(x => x.Submit(fakeCommand))
                .Returns(new SimpleCommandResult(true));

            var controller = new ProjectsController(mockCommandBus.Object);
            var result = controller.Post(fakeCommand);

            result
                .Should().BeOfType<OkNegotiatedContentResult<ICommandResult>>()
                .Which.Content.Success.Should().BeTrue();
        }

        [TestMethod]
        public void Should_Return200_When_Command_Fail()
        {
            mockCommandBus.Setup(x => x.Validate(fakeCommand))
                .Returns<IEnumerable<ValidationResult>>(null);
            mockCommandBus.Setup(x => x.Submit(fakeCommand))
                .Returns(new SimpleCommandResult(false, "An error occurred"));

            var controller = new ProjectsController(mockCommandBus.Object);
            var result = controller.Post(fakeCommand);

            result
                .Should().BeOfType<OkNegotiatedContentResult<ICommandResult>>()
                .Which.Content.Success.Should().BeFalse();
            result
                .Should().BeOfType<OkNegotiatedContentResult<ICommandResult>>()
                .Which.Content.Reason.Should().Be("An error occurred");
        }
    }
}