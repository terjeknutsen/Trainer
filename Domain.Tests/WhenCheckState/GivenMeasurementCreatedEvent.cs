using System;
using System.Collections.Generic;
using SpecsFor;
using Moq;
using Should;
using NUnit.Framework;
using CoreDomain.States;
using DomainInterfaces;
using CoreDomain.Events;
using CoreDomain.Identity;

namespace Domain.Tests.WhenCheckState
{
    public class GivenMeasurementCreatedEvent : SpecsFor<MeasurementState>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new MeasurementState(new List<IEvent>
            {
                new MeasurementCreated(new MeasurementId(Guid.NewGuid()), "Stomach","cm", It.IsAny<DateTime>())
            });
        }
        [Test]
        public void Then_type_should_be_set()
        {
            SUT.Type.ShouldEqual("Stomach");
        }
        [Test]
        public void Then_measurement_id_should_be_set()
        {
            SUT.Id.ShouldNotBeNull();
        }
        [Test]
        public void Then_unit_should_be_set()
        {
            SUT.Unit.ShouldEqual("cm");
        }
    }
}
