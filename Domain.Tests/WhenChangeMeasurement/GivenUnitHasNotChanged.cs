using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecsFor;
using Moq;
using NUnit.Framework;
using Should;
using CoreDomain.States;
using DomainInterfaces;
using CoreDomain.Events;
using CoreDomain.Identity;
using CoreDomain;

namespace Domain.Tests.WhenChangeMeasurement
{
    public class GivenUnitHasNotChanged : SpecsFor<Measurement>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Measurement(new List<IEvent>
            {
                new MeasurementCreated(It.IsAny<MeasurementId>(),"Stomach","cm",It.IsAny<DateTime>())
            });
        }
        protected override void When()
        {
            SUT.ChangeUnit("cm", It.IsAny<DateTime>());
        }
        [Test]
        public void Then_change_unit_event_should_NOT_be_created()
        {
            SUT.Changes.ShouldBeEmpty();
        }
    }
}
