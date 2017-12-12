using System.ComponentModel;

namespace ProjectManagement.Domain
{
    /// <summary>
    /// Represents the current status of a Project
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// When the Project is created in the Application
        /// </summary>
        [Description("In Preparation")]
        InPreparation = 0,

        /// <summary>
        /// When TAFE started the discussion
        /// </summary>
        [Description("In Progress")]
        InProgress = 1,

        /// <summary>
        /// TAFC or TAF Manager have approved the Project
        /// </summary>
        [Description("Approved")]
        Approved = 2,

        /// <summary>
        /// Consultant has signed the contract
        /// </summary>
        [Description("On Going")]
        OnGoing = 3,

        /// <summary>
        /// The TA Project completed and got green light from TAFE
        /// </summary>
        [Description("TA project completed")]
        Completed = 4,

        /// <summary>
        /// The Consultant has been fully paid and PLI paid
        /// </summary>
        [Description("Consultant fully paid")]
        Closed = 5
    }
}