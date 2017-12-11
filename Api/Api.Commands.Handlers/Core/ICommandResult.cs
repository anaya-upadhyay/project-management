using ProjectManagement.Api.Commands.Core;

namespace ProjectManagement.Api.Handlers.Core
{
    /// <summary>
    /// Contract used to represent the result produced by the execution of a Command of type <see cref="ICommand"/>
    /// </summary>
    public interface ICommandResult
    {
        /// <summary>
        /// True if the command is executed succesfully
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Reason of failure, present when Success is true
        /// </summary>
        string Reason { get; }
    }
}