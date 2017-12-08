using System.Collections.Generic;
using ProjectManagement.Api.Commands.Core;
using ProjectManagement.Api.Handlers.Core;

namespace ProjectManagement.Api.Buses.Commands
{
    /// <summary>
    /// Contract to define a Bus capable to handle Commands and their validation
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// Submit a new Command and ask for execution
        /// </summary>
        /// <typeparam name="TCommand">The Type of Command to be executed</typeparam>
        /// <param name="command">The instance of the Command to be executed</param>
        /// <returns>Returns an implementation of an <see cref="ICommandResult"/></returns>
        ICommandResult Submit<TCommand>(TCommand command) where TCommand : class, ICommand;

        /// <summary>
        /// Validate a Command before its execution and returns a collection of validation results
        /// </summary>
        /// <typeparam name="TCommand">The Type of Command to be validated</typeparam>
        /// <param name="command">The instance of the Command to be validated</param>
        /// <returns>Returns a collection of Validation Results</returns>
        IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}