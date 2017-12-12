using System.Collections.Generic;
using ProjectManagement.Api.Handlers.Core;
using Conditions;
using ProjectManagement.Api.Commands.Core;
using ProjectManagement.Api.Commands.Handlers.Core;

namespace ProjectManagement.Api.Buses.Commands
{
    /// <summary>
    /// Synchronous Command Bus implementation
    /// </summary>
    public sealed class DefaultCommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory handlerFactory;
        private readonly IValidatorFactory validatorFactory;

        public DefaultCommandBus(ICommandHandlerFactory handlerFactory, IValidatorFactory validatorFactory)
        {
            this.handlerFactory = handlerFactory;
            this.validatorFactory = validatorFactory;
        }

        /// <inheritdoc />
        public ICommandResult Submit<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            command.Requires(typeof(TCommand).FullName)
                .IsNotNull();

            var handler = handlerFactory.BuildHandler<TCommand>();

            handler.Requires(typeof(ICommandHandler<TCommand>).FullName)
                .IsNotNull();
            return handler.Execute(command);
        }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            command.Requires(typeof(TCommand).FullName)
                .IsNotNull();

            var validator = validatorFactory.BuildValidator<TCommand>();

            validator.Requires(typeof(IValidationHandler<TCommand>).FullName)
                .IsNotNull();
            return validator.Validate(command);
        }
    }
}