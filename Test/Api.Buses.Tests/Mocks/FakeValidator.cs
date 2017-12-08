using System.Collections.Generic;
using ProjectManagement.Api.Handlers.Core;

namespace ProjectManagement.Api.Buses.Tests.Mocks
{
    public class FakeValidator : IValidationHandler<FakeCommand>
    {
        public IEnumerable<ValidationResult> Validate(FakeCommand command)
        {
            return null;
        }
    }
}