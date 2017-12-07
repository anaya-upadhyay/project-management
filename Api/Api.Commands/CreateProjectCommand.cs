using System;
using Api.Commands.Core;

namespace Api.Commands
{
    /// <inheritdoc />
    /// <summary>
    ///     Create a new Project Root Aggregate
    /// </summary>
    public sealed class CreateProjectCommand : ICommand
    {
        /// <summary>
        ///     Create a new Project Root Aggregate
        /// </summary>
        /// <param name="donorId">The Unique Id of the Donor which disbursed the money for the Project</param>
        /// <param name="analystId">The Unique Id of the Analyst assigned to the Project</param>
        /// <param name="acronym">The Acronym of the Project</param>
        /// <param name="projectType">The Type of Project</param>
        /// <param name="tenderProcessType">The Tender Process Type</param>
        /// <param name="startDate">The eventual start date</param>
        public CreateProjectCommand(Guid donorId, Guid analystId, string acronym, string projectType,
            string tenderProcessType, DateTime? startDate)
        {
            DonorId = donorId;
            AnalystId = analystId;
            Acronym = acronym;
            ProjectType = projectType;
            TenderProcessType = tenderProcessType;
            StartDate = startDate;
        }

        /// <summary>
        ///     The Unique Id of the Donor which disbursed the money for the Project
        /// </summary>
        public Guid DonorId { get; }

        /// <summary>
        ///     The Unique Id of the Analyst assigned to the Project
        /// </summary>
        public Guid AnalystId { get; }

        /// <summary>
        ///     The Acronym of the Project
        /// </summary>
        public string Acronym { get; }

        /// <summary>
        ///     The Type of Project
        /// </summary>
        public string ProjectType { get; }

        /// <summary>
        ///     The Tender Process Type
        /// </summary>
        public string TenderProcessType { get; }

        /// <summary>
        ///     The eventual start date
        /// </summary>
        public DateTime? StartDate { get; }
    }
}