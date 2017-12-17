using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecsFor;
using Moq;
using Should;
using NUnit.Framework;
using CoreDomain;
using DomainInterfaces;
using CoreDomain.Events;
using CoreDomain.Identity;

namespace Domain.Tests.WhenAddValueToMeasurement
{
    public class GivenAnyMeasurement : SpecsFor<Measurement>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Measurement(new List<IEvent>
            {
                new MeasurementCreated(It.IsAny<MeasurementId>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>())
            });
        }
        protected override void When()
        {
            SUT.AddValue(22.5,It.IsAny<DateTime>());
        }
        [Test]
        public void Then_measurement_value_added_event_should_be_created()
        {
            SUT.Changes.First().ShouldBeType(typeof(MeasurementValueAdded));
        }
    }
}
