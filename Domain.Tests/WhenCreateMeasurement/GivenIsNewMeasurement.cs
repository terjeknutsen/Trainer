using System.Collections.Generic;
using SpecsFor;
using Moq;
using CoreDomain;
using DomainInterfaces;
using CoreDomain.Identity;
using NUnit.Framework;
using System.Linq;
using Should;
using CoreDomain.Events;

namespace Domain.Tests.WhenCreateMeasurement
{
    public class GivenIsNewMeasurement : SpecsFor<Measurement>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Measurement(new List<IEvent>());
        }
        protected override void When()
        {
            SUT.Create(It.IsAny<MeasurementId>(),"Stomach","cm");
        }
        [Test]
        public void Then_measurement_created_event_should_be_created()
        {
            SUT.Changes.First().ShouldBeType(typeof(MeasurementCreated));
        }
    }
}
