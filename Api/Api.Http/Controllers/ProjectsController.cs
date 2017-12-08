using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ProjectManagement.Api.Buses.Commands;
using ProjectManagement.Api.Commands;
using ProjectManagement.Domain;

namespace ProjectManagement.Api.Http.Controllers
{
    /// <summary>
    ///     Provide APIs to interact with a Project Root Aggregate
    /// </summary>
    public class ProjectsController : ApiController
    {

        private readonly ICommandBus commandBus;

        /// <inheritdoc />
        /// <summary>
        /// Create a new Instance of a Projects Controller with the injected services
        /// </summary>
        /// <param name="commandBus">The Current configured Command Bus</param>
        public ProjectsController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        /// <summary>
        /// Create a new Project Aggregate with all the required information
        /// </summary>
        /// <param name="command">The command of type <see cref="CreateProjectCommand"/> used to create the Project</param>
        [HttpPost]
        [Route("projects")]
        public IHttpActionResult Post(CreateProjectCommand command)
        {
            var errors = commandBus.Validate(command);

            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Name, error.Reason);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = commandBus.Submit(command);
            return Ok(result);
        }
    }
}