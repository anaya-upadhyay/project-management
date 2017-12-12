namespace ProjectManagement.Domain
{
    /// <summary>
    ///     Represents a Symbiotics Analyst employee
    /// </summary>
    public sealed class Analyst : Person
    {
        /// <summary>
        ///     For OR/M usage
        /// </summary>
        private Analyst()
        {
        }

        /// <inheritdoc />
        public Analyst(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }
    }
}