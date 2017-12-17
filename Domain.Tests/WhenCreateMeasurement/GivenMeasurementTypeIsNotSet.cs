using System;
using System.Collections.Generic;
using SpecsFor;
using Moq;
using CoreDomain;
using DomainInterfaces;
using NUnit.Framework;
using CoreDomain.Identity;

namespace Domain.Tests.WhenCreateMeasurement
{
    public class GivenMeasurementTypeIsNotSet : SpecsFor<Measurement>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Measurement(new List<IEvent>());
        }
        [Test]
        public void Then_invalid_operation_exception_should_be_raised()
        {
            Assert.Throws<ArgumentNullException>(()=> SUT.Create(It.IsAny<MeasurementId>(),null,"cm"));
        }
    }
}
