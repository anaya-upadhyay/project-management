namespace ProjectManagement.Domain.Events
{
    /// <summary>
    ///     Raised when a new Project is created and initialized
    /// </summary>
    public class ProjectCreated : IDomainEvent
    {
        /// <summary>
        ///     The Newly created Project
        /// </summary>
        public ProjectAggregate Project { get; set; }
    }
}