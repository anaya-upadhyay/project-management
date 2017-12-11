using ProjectManagement.Api.Commands.Core;
using ProjectManagement.Api.Commands.Handlers.Core;
using ProjectManagement.Api.Handlers.Core;

namespace ProjectManagement.Api.Buses.Commands
{
    /// <summary>
    /// Resolve an available and registered Command Handler
    /// </summary>
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// Build a new Command Handler
        /// </summary>
        /// <typeparam name="TCommand">The Type of Command to be Executed</typeparam>
        /// <returns>Return a valid instance of a Command Handler</returns>
        ICommandHandler<TCommand> BuildHandler<TCommand>() where TCommand : ICommand;
    }
}