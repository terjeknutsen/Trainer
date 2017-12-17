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

namespace Domain.Tests.WhenCreateMeasurement
{
    public class GivenMeasurementAlreadyCreated : SpecsFor<Measurement>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Measurement(new List<IEvent>
            {
                new MeasurementCreated(It.IsAny<MeasurementId>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>())
            });
        }

        [Test]
        public void Then_an_invalid_operation_exception_should_be_raised()
        {
            Assert.Throws<InvalidOperationException>(() => SUT.Create(It.IsAny<MeasurementId>(), It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
