using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Domain.Events;

namespace ProjectManagement.Domain.Tests
{
    [TestClass]
    public class DomainEventsTests
    {
        [TestMethod]
        public void Should_AddEvent_ToListOf_RegisteredEvents()
        {
            DomainEvents.Register<FakeEvent>(x => Console.Write("Registered"));

            var actions = (List<Delegate>) typeof(DomainEvents).GetField("actions", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            actions.Should().HaveCount(1);           
        }

        [TestMethod]
        public void Should_RemoveEvent_ToListOf_RegisteredEvents()
        {
            DomainEvents.Register<FakeEvent>(x => Console.Write("Registered"));
            DomainEvents.ClearCallbacks();

            var actions = (List<Delegate>)typeof(DomainEvents).GetField("actions", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            actions.Should().HaveCount(0);
        }
    }

    /// <summary>
    /// Fake event to be tested
    /// </summary>
    public class FakeEvent : IDomainEvent { }
}