using Conditions;

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
        public Analyst(string firstName, string lastName, string acronym)
            : base(firstName, lastName)
        {
            acronym.Requires("Acronym").IsNotNullOrEmpty();

            this.Acronym = acronym;
        }

        /// <inheritdoc />
        public override string Display => $"{LastName}, {FirstName} ({Acronym})";

        /// <summary>
        /// The Acronym used for each Analyst
        /// </summary>
        public string Acronym { get; }
    }
}