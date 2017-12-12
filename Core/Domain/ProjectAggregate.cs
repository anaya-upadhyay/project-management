using System;
using Conditions;
using ProjectManagement.Domain.Core;
using ProjectManagement.Domain.Events;

namespace ProjectManagement.Domain
{
    /// <inheritdoc cref="IEntity" />
    /// <inheritdoc cref="IDeletable" />
    /// <summary>
    ///     Represents a Project Aggregate
    /// </summary>
    public sealed class ProjectAggregate : IEntity, IDeletable
    {
        /// <summary>
        ///     For OR/M usage
        /// </summary>
        private ProjectAggregate()
        {
        }

        /// <summary>
        ///     Create a new Project Aggregate instance
        /// </summary>
        /// <param name="donor">The Donor which funded the Project</param>
        /// <param name="analyst">The Analyst associated to the Project</param>
        /// <param name="acronym">The Acronym of the Project</param>
        /// <param name="projectType">The Type of Project</param>
        /// <param name="tenderProcessType">The Tender Process applied to the Project</param>
        /// <param name="startDate">The estimated start date of the Project</param>
        public ProjectAggregate(DonorAggregate donor, Analyst analyst, string acronym, TypeOfProject projectType,
            TypeOfTenderProcess tenderProcessType, DateTime? startDate = null)
        {
            // pre-conditions
            donor.Requires("Donor").IsNotNull();
            acronym.Requires("Acronym").IsNotNullOrEmpty();
            analyst.Requires("Analyst").IsNotNull();

            // initialization
            Id = Guid.NewGuid();
            Status = Status.InPreparation;
            Donor = donor;
            Acronym = acronym;
            ProjectType = projectType;
            TenderProcessType = tenderProcessType;
            Analyst = analyst;
            ExpectedStartDate = startDate ?? DateTime.UtcNow;

            // TODO
            TierLevel = TierLevel.Tier1;

            // associations
            Donor.AssignProjectToDonor(this);

            // events
            DomainEvents.Raise(new ProjectCreated {Project = this});
        }

        /// <summary>
        ///     The Type of Tender Process applied to the Project
        /// </summary>
        public TypeOfTenderProcess TenderProcessType { get; }

        /// <summary>
        ///     The Initial Start Date set to the project, also known as "Expected"
        /// </summary>
        public DateTime ExpectedStartDate { get; }

        /// <summary>
        ///     The Analyst responsible of the Project
        /// </summary>
        public Analyst Analyst { get; }

        /// <summary>
        ///     The Donor in charge of disbursing the amount required by the Project
        /// </summary>
        public DonorAggregate Donor { get; }

        /// <summary>
        ///     The Acronym which represents a Project
        /// </summary>
        public string Acronym { get; }

        /// <summary>
        ///     The Type of Project used to classify
        /// </summary>
        public TypeOfProject ProjectType { get; }

        /// <summary>
        ///     Implementation of the Soft Delete contract
        /// </summary>
        public bool IsDeleted { get; private set; }

        /// <summary>
        ///     Soft delete the record
        /// </summary>
        public void SetDeleted()
        {
            IsDeleted = true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     The Unique Id of the Project
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The Current Status of the Project
        /// </summary>
        public Status Status { get; }

        /// <summary>
        /// The Tier Level of the Project, related to the Total Asset of the PLI involved in the Project
        /// </summary>
        public TierLevel TierLevel { get; }
    }
}