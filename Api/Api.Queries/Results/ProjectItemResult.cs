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
        /// <param name="donor">The Donor whom sponsored the Project</param>
        public ProjectItemResult(Guid id, string acronym, string donor)
        {
            Id = id;
            Acronym = acronym;
            Donor = donor;
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
        /// The Donor whom sponsored the Project
        /// </summary>
        public string Donor { get; }
    }
}