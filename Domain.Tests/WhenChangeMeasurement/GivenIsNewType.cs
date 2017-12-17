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
   public class GivenIsNewType: SpecsFor<Measurement>
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
            SUT.ChangeType("Stomach",It.IsAny<DateTime>());
        }
        [Test]
        public void Then_measurement_type_changed_event_should_be_created()
        {
            SUT.Changes.First().ShouldBeType(typeof(MeasurementTypeChanged));
        }
    }
}
