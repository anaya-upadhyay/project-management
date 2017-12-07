using Api.Commands.Core;

namespace ProjectManagement.Api.Handlers.Core
{
    /// <summary>
    /// Contract used to handle a specific command
    /// </summary>
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        ICommandResult Execute(TCommand command);
    }
}