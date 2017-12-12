using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using ProjectManagement.Api.Buses.Commands;
using ProjectManagement.Api.Commands;
using ProjectManagement.Api.Handlers.Core;
using ProjectManagement.Api.Queries;
using ProjectManagement.Api.Queries.Core;
using ProjectManagement.Api.Queries.Results;

namespace ProjectManagement.Api.Http.Controllers
{
    /// <summary>
    ///     Provide APIs to interact with a Project Root Aggregate
    /// </summary>
    public class ProjectsController : ApiController
    {
        private readonly IBus bus;

        /// <inheritdoc />
        /// <summary>
        ///     Create a new Instance of a Projects Controller with the injected services
        /// </summary>
        /// <param name="bus">The Current configured Command Bus</param>
        public ProjectsController(IBus bus)
        {
            this.bus = bus;
        }

        /// <summary>
        ///     Create a new Project Aggregate with all the required information
        /// </summary>
        /// <param name="command">The command of type <see cref="CreateProjectCommand" /> used to create the Project</param>
        [HttpPost]
        [ResponseType(typeof(SimpleCommandResult))]
        [Route("projects")]
        public IHttpActionResult Post(CreateProjectCommand command)
        {
            var errors = bus.Validate(command);

            foreach (var error in errors ?? Enumerable.Empty<ValidationResult>())
            {
                ModelState.AddModelError(error.Name, error.Reason);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = bus.Submit(command);
            return Ok(result);
        }

        /// <summary>
        /// Search for Projects according to the Search Criteria and return a paginated result
        /// </summary>
        /// <param name="query">The <see cref="SearchProjectsQuery"/> query to be executed</param>
        /// <returns>Returns a collection of <see cref="IPaginatedResult{ProjectItemResult}"/> result</returns>
        [HttpGet]
        [ResponseType(typeof(IPaginatedResult<ProjectItemResult>))]
        [Route("projects")]
        public IHttpActionResult Get(SearchProjectsQuery query)
        {
            var result = bus.Query<SearchProjectsQuery, IPaginatedResult<ProjectItemResult>>(query);
            return Ok(result);
        }
    }
}