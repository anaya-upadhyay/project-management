using System;

namespace ProjectManagement.Domain.Core
{
    /// <summary>
    ///     Represent an Entity inside the Domain
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        ///     The Unique Identifier of the Entity
        /// </summary>
        Guid Id { get; }
    }
}