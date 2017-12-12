using System;
using Conditions;
using ProjectManagement.Domain.Core;

namespace ProjectManagement.Domain
{
    /// <summary>
    ///     Represents a generic Person with a FirstName and a LastName
    /// </summary>
    public abstract class Person : IEntity
    {
        /// <summary>
        ///     For OR/M usage
        /// </summary>
        protected Person()
        {
        }

        /// <summary>
        ///     Create and Initialize a new Person record
        /// </summary>
        /// <param name="firstName">The FirstName to be assigned</param>
        /// <param name="lastName">The LastName to be assigned</param>
        protected Person(string firstName, string lastName)
        {
            // pre-conditions
            firstName.Requires("FirstName").IsNotNullOrEmpty();
            lastName.Requires("LastName").IsNotNullOrEmpty();

            // inizialitation
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        ///     The FirstName of the Person
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        ///     The LastName of the Person
        /// </summary>
        public string LastName { get; }

        /// <summary>
        ///     The Unique Id of the Entity
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The Display Name used to display an instance of a Person
        /// </summary>
        /// <remarks>Overridden by the child class</remarks>
        public abstract string Display { get; }
    }
}