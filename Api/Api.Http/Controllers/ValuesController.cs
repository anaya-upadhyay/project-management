using System;
using System.Collections.Generic;
using System.Web.Http;
using ProjectManagement.Domain;

namespace Api.Http.Controllers
{
    /// <summary>
    ///     Provide APIs to interact with a Project Root Aggregate
    /// </summary>
    public class ProjectsController : ApiController
    {
        /// <summary>
        ///     Returns a list of Projects without any children value object
        /// </summary>
        /// <returns>Returns a list of flattern Project aggregates</returns>
        [HttpGet]
        [Route("projects")]
        public IEnumerable<ProjectAggregate> Get()
        {
            return null;
        }

        /// <summary>
        ///     Return a specific Project Aggregate including its children value objects
        /// </summary>
        /// <param name="projectId">The Unique Id of the Project</param>
        /// <returns>Return an object of type <see cref="ProjectAggregate" /></returns>
        [HttpGet]
        [Route("projects/{projectId}")]
        public ProjectAggregate Get(Guid projectId)
        {
            return null;
        }

        /// <summary>
        /// Return the Analyst assigned to the project
        /// </summary>
        /// <param name="projectId">The Unique Id of the Project</param>
        /// <returns>Return an object of type <see cref="Analyst"/></returns>
        [HttpGet]
        [Route("projects/{projectId}/analyst")]
        public Analyst GetAnalyst(Guid projectId)
        {
            return null;
        }

        /// <summary>
        /// Deactivate a Project and mark it as deleted
        /// </summary>
        /// <param name="projectId">The Unique Id of the Project</param>
        /// <remarks>The Project is not physically deleted, but de-activated</remarks>
        [HttpDelete]
        [Route("projects/{projectId}")]
        public void Delete(Guid projectId)
        {
            
        }
    }
}