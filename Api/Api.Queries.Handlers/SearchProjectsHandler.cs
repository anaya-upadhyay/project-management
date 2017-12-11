using System.Linq;
using ProjectManagement.Api.Queries.Core;
using ProjectManagement.Api.Queries.Handlers.Core;
using ProjectManagement.Api.Queries.Results;
using ProjectManagement.Dal;
using ProjectManagement.Domain;

namespace ProjectManagement.Api.Queries.Handlers
{
    /// <summary>
    /// Search for Projects using a custom text criteria and return a paginated result
    /// </summary>
    public class SearchProjectsHandler : IQueryHandler<SearchProjectsQuery,IPaginatedResult<ProjectItemResult>>
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Create a new Project Handler with an active Unit of Work
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SearchProjectsHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public IPaginatedResult<ProjectItemResult> Handle(SearchProjectsQuery query)
        {
            var repository = unitOfWork.CreateRepository();

            var queryable = repository.Get<ProjectAggregate>()
                .Where(x => x.Acronym.Contains(query.Text));

            var items = queryable
                .Skip(query.Records * (query.Page - 1))
                .Take(query.Records)
                .Select(x => new ProjectItemResult(x.Id, x.Acronym, x.Donor.Name))
                .ToArray();

            return new PaginatedResult<ProjectItemResult>(query.Page, queryable.Count(), items);
        }
    }
}