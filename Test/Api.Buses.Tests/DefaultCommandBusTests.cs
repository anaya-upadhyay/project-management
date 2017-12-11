using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectManagement.Api.Buses.Commands;
using ProjectManagement.Api.Buses.Tests.Mocks;
using ProjectManagement.Api.Commands.Handlers.Core;
using ProjectManagement.Api.Handlers.Core;

namespace ProjectManagement.Api.Buses.Tests
{
    [TestCategory("Api.Buses")]
    [TestClass]
    public class DefaultCommandBusTests
    {
        private ICommandBus bus;
        private Mock<ICommandHandlerFactory> mockHandlerFactory;
        private Mock<IValidatorFactory> mockValidatorFactory;

        [TestInitialize]
        public void Initialize()
        {
            // initialization of mocks
            mockHandlerFactory = new Mock<ICommandHandlerFactory>();
            mockValidatorFactory = new Mock<IValidatorFactory>();

            mockHandlerFactory.Setup(x => x.BuildHandler<FakeCommand>())
                .Returns(new FakeHandler());
            mockHandlerFactory.Setup(x => x.BuildHandler<FakeNotImplementedCommand>())
                .Returns(default(ICommandHandler<FakeNotImplementedCommand>));
            mockValidatorFactory.Setup(x => x.BuildValidator<FakeCommand>())
                .Returns(new FakeValidator());
            mockValidatorFactory.Setup(x => x.BuildValidator<FakeNotImplementedCommand>())
                .Returns(default(IValidationHandler<FakeNotImplementedCommand>));

            bus = new DefaultCommandBus(mockHandlerFactory.Object, mockValidatorFactory.Object);
        }


        [TestMethod]
        public void Should_ThrowException_When_Handler_IsNotAvailable()
        {
            Action action = () => bus.Submit(new FakeNotImplementedCommand());

            action.ShouldThrow<ArgumentNullException>()
                .Which.ParamName.Equals(typeof(ICommandHandler<FakeNotImplementedCommand>).FullName);
        }

        [TestMethod]
        public void Should_ThrowException_When_Validator_IsNotAvailable()
        {
            Action action = () => bus.Validate(new FakeNotImplementedCommand());

            action.ShouldThrow<ArgumentNullException>()
                .Which.ParamName.Equals(typeof(ICommandHandler<FakeNotImplementedCommand>).FullName);
        }

        [TestMethod]
        public void Should_ThrowException_When_Submit_ANullCommand()
        {
            Action action = () => bus.Submit(new FakeNotImplementedCommand());

            action.ShouldThrow<ArgumentNullException>()
                .Which.ParamName.Equals(typeof(FakeNotImplementedCommand).FullName);
        }

        [TestMethod]
        public void Should_ThrowException_When_Validating_ANullCommand()
        {
            Action action = () => bus.Validate(new FakeNotImplementedCommand());

            action.ShouldThrow<ArgumentNullException>()
                .Which.ParamName.Equals(typeof(FakeNotImplementedCommand).FullName);
        }

        [TestMethod]
        public void Should_ExecuteCommand_When_Handler_IsAvailable()
        {
            var result = bus.Submit(new FakeCommand());
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void Should_ValidateCommand_When_Validator_IsAvailable()
        {
            var validation = bus.Validate(new FakeCommand());
            validation.Should().BeNull();
        }
    }
}
