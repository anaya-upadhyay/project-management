using ProjectManagement.Api.Commands.Core;
using ProjectManagement.Api.Handlers.Core;

namespace ProjectManagement.Api.Buses.Commands
{
    /// <summary>
    /// Resolve an available and registered Command Validator
    /// </summary>
    public interface IValidatorFactory 
    {
        /// <summary>
        /// Build a new Command Validator
        /// </summary>
        /// <typeparam name="TCommand">The Type of Command to be Validated</typeparam>
        /// <returns>Returns a valid instance of a Command Validator</returns>
        IValidationHandler<TCommand> BuildValidator<TCommand>() where TCommand : ICommand;
    }
}