using CoreDomain.Events;
using CoreDomain.Identity;
using CoreDomain.States;
using DomainInterfaces;
using Moq;
using NUnit.Framework;
using Should;
using SpecsFor;
using System;
using System.Collections.Generic;

namespace Domain.Tests.WhenCheckState
{
    public class GivenMeasurementTypeChangedEvent : SpecsFor<MeasurementState>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new MeasurementState(new List<IEvent>
            {
                new MeasurementCreated(It.IsAny<MeasurementId>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<DateTime>()),
                new MeasurementTypeChanged("Stomach", It.IsAny<DateTime>())
            });
        }
        [Test]
        public void Then_measurement_type_should_be_set()
        {
            SUT.Type.ShouldEqual("Stomach");
        }
    }
}
