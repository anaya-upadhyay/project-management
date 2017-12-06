using ProjectManagement.Domain.Core;

namespace ProjectManagement.Domain.Events
{
    /// <summary>
    /// Raised when a Project is modified
    /// </summary>
    public sealed class ProjectModified : IDomainEvent
    {
        /// <summary>
        /// The Modified Project
        /// </summary>
        public ProjectAggregate Project { get; set; }
    }
}