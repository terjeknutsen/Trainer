using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecsFor;
using Moq;
using NUnit.Framework;
using Should;
using CoreDomain;
using DomainInterfaces;
using CoreDomain.Events;
using CoreDomain.Identity;

namespace Domain.Tests.WhenChangeMeasurement
{
   public class GivenIsNewUnit : SpecsFor<Measurement>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Measurement(new List<IEvent>
            {
                new MeasurementCreated(It.IsAny<MeasurementId>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<DateTime>())
            });
        }

        protected override void When()
        {
            SUT.ChangeUnit("kg",It.IsAny<DateTime>());
        }
        [Test]
        public void Then_measurement_unit_changed_event_should_be_created()
        {
            SUT.Changes.First().ShouldBeType(typeof(MeasurementUnitChanged));
        }
    }
}
