using System;
using System.Threading.Tasks;
using SpecsFor;
using Moq;
using Should;
using PushUp.Application.EventHandlers;
using Nito.AsyncEx;
using CoreDomain.Identity;
using CoreDomain.Events;
using NUnit.Framework;
using ViewModels;
using StorageInterfaces;

namespace PushUp.Application.Test.Views.When_update_view
{
    class SpecsForMeasurementEventHandler
    {
        public class When_create_measurement : SpecsFor<MeasurementEventHandler>
        {
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(It.IsAny<MeasurementId>(), new MeasurementCreated(new MeasurementId(Guid.NewGuid()), "type", "unit", DateTime.Now)));
            }
            [Test]
            public void Then_measurement_view_model_should_be_added_to_repository()
            {
                GetMockFor<IRepository<MeasurementViewModel>>().Verify(r => r.AddAsync(It.IsAny<MeasurementViewModel>()), Times.Once);
            }
        }
        public class When_change_measurement_type : SpecsFor<MeasurementEventHandler>
        {
            protected override void Given()
            {
                Given<ViewModelExist>();
                Given<CallbackSetup>();
                base.Given();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(new MeasurementId(Guid.NewGuid()), new MeasurementTypeChanged("type", It.IsAny<DateTime>())));
            }
            [Test]
            public void Then_type_should_be_updated()
            {
                Callback.Type.ShouldEqual("type");
            }
        }
        public class When_change_measurement_unit : SpecsFor<MeasurementEventHandler>
        {
            protected override void Given()
            {
                Given<ViewModelExist>();
                Given<CallbackSetup>();
                base.Given();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(new MeasurementId(Guid.NewGuid()), new MeasurementUnitChanged("unit", It.IsAny<DateTime>())));
            }
            [Test]
            public void Then_unit_should_be_updated()
            {
                Callback.Unit.ShouldEqual("unit");
            }
        }
        public class When_add_measurement_value : SpecsFor<MeasurementEventHandler>
        {
            protected override void Given()
            {
                Given<ViewModelExist>();
                Given<CallbackSetup>();
                base.Given();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.Update(new MeasurementId(Guid.NewGuid()), new MeasurementValueAdded(98.3, new DateTime(2017,2,2,3,2,2))));
            }
            [Test]
            public void Then_measurement_value_should_be_added()
            {
                Callback.Values.ContainsKey(new DateTime(2017, 2, 2, 3, 2, 2)).ShouldBeTrue();
            }
        }

        class ViewModelExist : IContext<MeasurementEventHandler>
        {
            public void Initialize(ISpecs<MeasurementEventHandler> state)
            {
                state.GetMockFor<IRepository<MeasurementViewModel>>().Setup(r => r.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new MeasurementViewModel()));
            }
        }
        static MeasurementViewModel Callback { get; set; }
        class CallbackSetup : IContext<MeasurementEventHandler>
        {
            public void Initialize(ISpecs<MeasurementEventHandler> state)
            {
                state.GetMockFor<IRepository<MeasurementViewModel>>().Setup(r => r.UpdateAsync(It.IsAny<MeasurementViewModel>()))
                    .Callback((MeasurementViewModel m) => Callback = m)
                    .Returns(Task.CompletedTask);
            }
        }
    }
}
