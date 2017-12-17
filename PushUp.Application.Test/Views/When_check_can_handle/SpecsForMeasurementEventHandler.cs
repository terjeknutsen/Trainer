using CoreDomain.Events;
using CoreDomain.Identity;
using Moq;
using NUnit.Framework;
using PushUp.Application.EventHandlers;
using Should;
using SpecsFor;
using System;

namespace PushUp.Application.Test.Views.When_check_can_handle
{
    class SpecsForMeasurementEventHandler
    {
        public class Given_events_setup : SpecsFor<MeasurementEventHandler>
        {
            [Test]
            public void Then_can_handle_measurement_created_should_be_true()
            {
                SUT.CanHandle(new MeasurementCreated(It.IsAny<MeasurementId>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<DateTime>())).ShouldBeTrue();
            }
            [Test]
            public void Then_can_handle_measurement_type_changed()
            {
                SUT.CanHandle(new MeasurementTypeChanged(It.IsAny<string>(), It.IsAny<DateTime>())).ShouldBeTrue();
            }
            [Test]
            public void Then_can_handle_measurement_unit_changed()
            {
                SUT.CanHandle(new MeasurementUnitChanged(It.IsAny<string>(), It.IsAny<DateTime>())).ShouldBeTrue();
            }
            [Test]
            public void Then_can_handle_measurement_value_added()
            {
                SUT.CanHandle(new MeasurementValueAdded(It.IsAny<double>(), It.IsAny<DateTime>())).ShouldBeTrue();
            }
        }
    }
}
