using System;
using System.Collections.Generic;

namespace ProjectManagement.Domain.Events
{
    public static class DomainEvents
    {
        [ThreadStatic] //so that each thread has its own callbacks
        private static List<Delegate> actions;

        // public static IContainer Container { get; set; } //as before

        /// <summary>
        ///     Registers a callback for the given domain event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
            {
                actions = new List<Delegate>();
            }
            actions.Add(callback);
        }

        /// <summary>
        ///     Clears callbacks passed to Register on the current thread
        /// </summary>
        public static void ClearCallbacks()
        {
            actions = null;
        }

        /// <summary>
        ///     Raises the given domain event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        public static void Raise<T>(T args) where T : IDomainEvent
        {
            // if (Container != null)
            // {
            //     foreach (var handler in Container.ResolveAll<Handles<T>>())
            //     {
            //         handler.Handle(args);
            //     }
            // }

            if (actions != null)
            {
                foreach (var action in actions)
                {
                    if (action is Action<T>)
                    {
                        ((Action<T>) action)(args);
                    }
                }
            }
        }
    }
}