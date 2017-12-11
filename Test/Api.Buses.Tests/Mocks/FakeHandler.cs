using System;
using ProjectManagement.Api.Commands.Handlers.Core;
using ProjectManagement.Api.Handlers.Core;

namespace ProjectManagement.Api.Buses.Tests.Mocks
{
    public class FakeHandler : ICommandHandler<FakeCommand>
    {
        public ICommandResult Execute(FakeCommand command)
        {
            Console.WriteLine($"Command {typeof(FakeCommand).FullName} handled");
            return new SimpleCommandResult(true);
        }
    }
}