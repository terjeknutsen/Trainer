using Moq;
using Nito.AsyncEx;
using NUnit.Framework;
using PushUp.Application.Commands;
using SpecsFor;
using StorageInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace PushUp.Application.Test
{
    class MeasurementServiceSpecs
    {
        static void InitSut(SpecsFor<MeasurementService> state, params IContext<MeasurementService>[] contexts)
        {
            foreach(var context in contexts)
            {
                context.Initialize(state);
            }
            state.SUT = new MeasurementService(state.GetMockFor<IEventStore>().Object, state.GetMockFor<IRepository<MeasurementViewModel>>().Object); 
        }
        public class When_create_measurement : SpecsFor<MeasurementService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this);
            }
            protected override void AfterEachTest()
            {
                GetMockFor<IEventStore>().Reset();
                GetMockFor <IRepository<MeasurementViewModel>>().Reset();
                base.AfterEachTest();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new CreateMeasurement(Guid.NewGuid(), "type", "unit")));
            }
            [Test]
            public void Then_view_model_should_be_updated()
            {
                GetMockFor<IRepository<MeasurementViewModel>>().Verify(r => r.AddAsync(It.IsAny<MeasurementViewModel>()), Times.Once);
            }

        }
        public class When_set_measurement_type : SpecsFor<MeasurementService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this, new ViewModelExist());
            }
            protected override void AfterEachTest()
            {
                GetMockFor<IEventStore>().Reset();
                GetMockFor<IRepository<MeasurementViewModel>>().Reset();
                base.AfterEachTest();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new SetMeasurementType(Guid.NewGuid(), "new type")));
            }
            [Test]
            public void Then_view_model_should_be_updated()
            {
                GetMockFor<IRepository<MeasurementViewModel>>().Verify(r => r.UpdateAsync(It.IsAny<MeasurementViewModel>()), Times.Once);
            }
        }
        public class When_set_measurement_unit : SpecsFor<MeasurementService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this, new ViewModelExist());
            }
            protected override void AfterEachTest()
            {
                GetMockFor<IEventStore>().Reset();
                GetMockFor<IRepository<MeasurementViewModel>>().Reset();
                base.AfterEachTest();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new SetMeasurementUnit(Guid.NewGuid(), "new unit")));
            }
            [Test]
            public void Then_view_model_should_be_updated()
            {
                GetMockFor<IRepository<MeasurementViewModel>>().Verify(r => r.UpdateAsync(It.IsAny<MeasurementViewModel>()), Times.Once);
            }
        }

        public class When_add_measurement_value : SpecsFor<MeasurementService>
        {
            protected override void InitializeClassUnderTest()
            {
                InitSut(this, new ViewModelExist());
            }
            protected override void AfterEachTest()
            {
                GetMockFor<IEventStore>().Reset();
                GetMockFor<IRepository<MeasurementViewModel>>().Reset();
                base.AfterEachTest();
            }
            protected override void When()
            {
                AsyncContext.Run(async () => await SUT.ExecuteCommand(new AddMeasurementValue(Guid.NewGuid(), 33)));
            }
            [Test]
            public void Then_view_model_should_be_updated()
            {
                GetMockFor<IRepository<MeasurementViewModel>>().Verify(r => r.UpdateAsync(It.IsAny<MeasurementViewModel>()), Times.Once);
            }
        }


        class ViewModelExist : IContext<MeasurementService>
        {
            public void Initialize(ISpecs<MeasurementService> state)
            {
                state.GetMockFor<IRepository<MeasurementViewModel>>().Setup(r => r.GetAsync(It.IsAny<Guid>()))
                    .Returns(Task.FromResult(new MeasurementViewModel()));
            }
        }
    }
}
