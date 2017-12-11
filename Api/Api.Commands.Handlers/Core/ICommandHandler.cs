using ProjectManagement.Api.Handlers.Core;

namespace ProjectManagement.Api.Commands.Handlers.Core
{
    /// <summary>
    /// Contract used to handle a specific command
    /// </summary>
    public interface ICommandHandler<in TCommand>
    {
        ICommandResult Execute(TCommand command);
    }
}