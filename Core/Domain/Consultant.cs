namespace ProjectManagement.Domain
{
    /// <summary>
    /// Represents a Consultant hired for one or more Projects
    /// </summary>
    public sealed class Consultant : Person
    {
        /// <summary>
        ///     For OR/M usage
        /// </summary>
        private Consultant()
        {
        }

        /// <inheritdoc />
        public Consultant(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }
    }
}