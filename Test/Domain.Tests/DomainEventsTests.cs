﻿using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Domain.Core;
using ProjectManagement.Domain.Events;

namespace ProjectManagement.Domain.Tests
{
    [TestCategory("Domain")]
    [TestClass]
    public class DomainEventsTests
    {
        [TestMethod]
        public void Should_InitializeAction_When_FirstRegister()
        {
            DomainEvents.Register<FakeEvent>(x => Console.Write("Registered"));
            var actions = (List<Delegate>)typeof(DomainEvents).GetField("actions", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

            actions.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_AddEvent_ToListOf_RegisteredEvents()
        {
            DomainEvents.ClearCallbacks();
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