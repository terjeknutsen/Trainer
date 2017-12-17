using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class GivenMeasurementValueAddedEvent : SpecsFor<MeasurementState>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new MeasurementState(new List<IEvent>
            {
                new MeasurementCreated(It.IsAny<MeasurementId>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<DateTime>()),
                new MeasurementValueAdded(22.2, It.IsAny<DateTime>())
            });
        }

        [Test]
        public void Then_value_should_be_added_to_dictionary()
        {
            SUT.Values.Values.ShouldContain(22.2);
        }
        
    }
}
