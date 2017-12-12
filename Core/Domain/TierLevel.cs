using System.ComponentModel;

namespace ProjectManagement.Domain
{
    /// <summary>
    /// Represents the Tier Level of a Project, assigned depending on the budget of the project
    /// </summary>
    public enum TierLevel
    {
        /// <summary>
        /// If the Insitution size is over 30M
        /// </summary>
        [Description("Tier 1")]
        Tier1 = 0,

        /// <summary>
        /// If the Institution size is between 10M and 30M
        /// </summary>
        [Description("Tier 2")]
        Tier2 = 1,

        /// <summary>
        /// If the Institution size is less than 10M
        /// </summary>
        [Description("Tier 3")]
        Tier3 = 2
    }
}