using System;
using Conditions;
using ProjectManagement.Domain.Events;

namespace ProjectManagement.Domain
{
    /// <summary>
    ///     Represents a Project Aggregate
    /// </summary>
    public sealed class ProjectAggregate : IEntity
    {
        /// <summary>
        ///     Create a new Project Aggregate instance
        /// </summary>
        /// <param name="donor">The Donor which funded the Project</param>
        /// <param name="acronym">The Acronym of the Project</param>
        public ProjectAggregate(Donor donor, string acronym)
        {
            // pre-conditions
            donor.Requires("Donor").IsNotNull();
            acronym.Requires("Acronym").IsNotNullOrEmpty();

            // initialization
            Id = Guid.NewGuid();
            Donor = donor;
            Acronym = acronym;

            // associations
            Donor.AssignProjectToDonor(this);

            // events
            DomainEvents.Raise(new ProjectCreated {Project = this});
        }

        /// <summary>
        ///     The Donor in charge of disbursing the amount required by the Project
        /// </summary>
        public Donor Donor { get; }

        /// <summary>
        ///     The Acronym which represents a Project
        /// </summary>
        public string Acronym { get; }

        /// <summary>
        ///     The Unique Id of the Project
        /// </summary>
        public Guid Id { get; }
    }
}