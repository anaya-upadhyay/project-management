using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Engine;
using NHibernate.Event;
using ProjectManagement.Domain.Core;

namespace ProjectManagement.Dal.Nhb.Listeners
{
    public class SoftDeletableListener : IPreDeleteEventListener
    {
        #region IPreDeleteEventListener Members

        [ExcludeFromCodeCoverage]
        public Task<bool> OnPreDeleteAsync(PreDeleteEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool OnPreDelete(PreDeleteEvent @event)
        {
            if (!(@event.Entity is IDeletable deletable))
            {
                return false;
            }

            var entry = @event.Session.GetSessionImplementation().PersistenceContext.GetEntry(@event.Entity);
            entry.Status = Status.Loaded;

            deletable.SetDeleted();

            var id = @event.Persister.GetIdentifier(@event.Entity);
            var fields = @event.Persister.GetPropertyValues(@event.Entity);
            var version = @event.Persister.GetVersion(@event.Entity);

            @event.Persister.Update(id, fields, new int[1], false, fields, version, @event.Entity, null,
                @event.Session.GetSessionImplementation());

            return (true);
        }

        #endregion
    }
}