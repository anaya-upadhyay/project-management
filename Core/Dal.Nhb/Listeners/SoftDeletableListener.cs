using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Engine;
using NHibernate.Event;
using ProjectManagement.Domain.Core;

namespace ProjectManagement.Dal.Nhb.Listeners
{
    public class SoftDeletableListener : IPreDeleteEventListener
{
    public void Register(Configuration cfg)
    {
        cfg.EventListeners.PreDeleteEventListeners = new IPreDeleteEventListener[] { this }.Concat(cfg.EventListeners.PreDeleteEventListeners).ToArray();
    }
 
    #region IPreDeleteEventListener Members

    public Task<bool> OnPreDeleteAsync(PreDeleteEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Boolean OnPreDelete(PreDeleteEvent @event)
     {
         if (!(@event.Entity is IDeletable deletable))
         {
             return false;
         }
  
         EntityEntry entry = @event.Session.GetSessionImplementation().PersistenceContext.GetEntry(@event.Entity);
         entry.Status = Status.Loaded;
  
         deletable.SetDeleted();
  
         Object id = @event.Persister.GetIdentifier(@event.Entity);
         Object[] fields = @event.Persister.GetPropertyValues(@event.Entity);
         Object version = @event.Persister.GetVersion(@event.Entity);
  
         @event.Persister.Update(id, fields, new Int32[1], false, fields, version, @event.Entity, null, @event.Session.GetSessionImplementation());
  
         return (true);
     }
  
     #endregion
 }}
