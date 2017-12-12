using ProjectManagement.Api.Queries.Handlers.Core;

namespace ProjectManagement.Api.Buses.Tests.Mocks
{
    public class FakeQueryHandler : IQueryHandler<FakeQuery, string>
    {
        public string Handle(FakeQuery query)
        {
            return "Hello World";
        }
    }
}