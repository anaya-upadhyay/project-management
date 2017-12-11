using System.Collections.Generic;
using ProjectManagement.Api.Commands.Core;

namespace ProjectManagement.Api.Handlers.Core
{
    /// <summary>
    ///     Command handler contract used exclusively to Validate a Command before it gets executed
    /// </summary>
    public interface IValidationHandler<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Validate a Command before it gets executed and return a list of Validation Errors
        /// </summary>
        /// <param name="command">The Command to be Validated</param>
        /// <returns>Returns a collection of ValidationResults related to the validation outcome of the Command</returns>
        IEnumerable<ValidationResult> Validate(TCommand command);
    }
}