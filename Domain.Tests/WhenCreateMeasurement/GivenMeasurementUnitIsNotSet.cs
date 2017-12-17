using System;
using System.Collections.Generic;
using SpecsFor;
using Moq;
using NUnit.Framework;
using CoreDomain;
using DomainInterfaces;
using CoreDomain.Identity;

namespace Domain.Tests.WhenCreateMeasurement
{
    public class GivenMeasurementUnitIsNotSet : SpecsFor<Measurement>
    {
        protected override void InitializeClassUnderTest()
        {
            SUT = new Measurement(new List<IEvent>());
        }
        [Test]
        public void Then_an_invalid_operation_exception_should_be_raised()
        {
            Assert.Throws<ArgumentNullException>(()=> SUT.Create(It.IsAny<MeasurementId>(),"Stomach",null));
        }
    }
}
