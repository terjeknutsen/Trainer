using System;
using System.Collections.Generic;
using SpecsFor;
using Moq;
using NUnit.Framework;
using Should;
using CoreDomain.States;
using DomainInterfaces;
using CoreDomain.Events;
using CoreDomain.Identity;

namespace Domain.Tests.WhenCheckState
{
    public class GivenMeasurementUnitChangedEvent : SpecsFor<MeasurementState>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new MeasurementState(new List<IEvent>
            {
                new MeasurementCreated(It.IsAny<MeasurementId>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>()),
                new MeasurementUnitChanged("kg",It.IsAny<DateTime>())
            });
        }
        [Test]
        public void Then_measurement_unit_should_be_set()
        {
            SUT.Unit.ShouldEqual("kg");
        }
    }
}
