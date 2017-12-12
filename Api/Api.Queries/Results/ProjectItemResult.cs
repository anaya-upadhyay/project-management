using System;

namespace ProjectManagement.Api.Queries.Results
{
    /// <summary>
    ///     Represents a Project Item Result, returned by a Search query
    /// </summary>
    public sealed class ProjectItemResult
    {
        /// <summary>
        /// Instantiate a new ProjectItemResult
        /// </summary>
        /// <param name="id">The Unique Id of the Project</param>
        /// <param name="acronym">The Acronym of the Project</param>
        /// <param name="analyst">The Analyst assigned to the Project</param>
        /// <param name="status">The Status of the Project</param>
        /// <param name="tierLevel">The Tier Level of the Project</param>
        public ProjectItemResult(Guid id, string acronym, string analyst, string status, string tierLevel)
        {
            Id = id;
            Acronym = acronym;
            Analyst = analyst;
            Status = status;
            TierLevel = tierLevel;
        }

        /// <summary>
        /// The Unique Id of the Project
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The Acronym of the Project
        /// </summary>
        public string Acronym { get; }

        /// <summary>
        /// The TA Analyst assigned to the Project
        /// </summary>
        public string Analyst { get; }

        /// <summary>
        /// The current status of the Project
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// The current TierLevel of the Project
        /// </summary>
        public string TierLevel { get; }
    }
}