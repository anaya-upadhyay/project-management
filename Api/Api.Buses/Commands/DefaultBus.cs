using System.Collections.Generic;
using ProjectManagement.Api.Handlers.Core;
using Conditions;
using ProjectManagement.Api.Commands.Core;
using ProjectManagement.Api.Commands.Handlers.Core;
using ProjectManagement.Api.Queries.Core;

namespace ProjectManagement.Api.Buses.Commands
{
    /// <summary>
    /// Synchronous Command Bus implementation
    /// </summary>
    public sealed class DefaultBus : IBus
    {
        private readonly ICommandHandlerFactory handlerFactory;
        private readonly IValidatorFactory validatorFactory;
        private readonly IQueryFactory queryFactory;

        public DefaultBus(ICommandHandlerFactory handlerFactory, IValidatorFactory validatorFactory, IQueryFactory queryFactory)
        {
            this.handlerFactory = handlerFactory;
            this.validatorFactory = validatorFactory;
            this.queryFactory = queryFactory;
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

        /// <inheritdoc />
        public TResult Query<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult> {
            query.Requires(typeof(TQuery).FullName)
                .IsNotNull();

            var handler = queryFactory.BuildHandler<TQuery, TResult>();

            handler.Requires(typeof(IQuery<TResult>).FullName)
                .IsNotNull();
            return handler.Handle(query);
        }
    }
}