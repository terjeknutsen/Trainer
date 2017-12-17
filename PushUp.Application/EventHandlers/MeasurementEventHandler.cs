using CoreDomain.Events;
using DomainInterfaces;
using StorageInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace PushUp.Application.EventHandlers
{
    public sealed class MeasurementEventHandler : EventHandler<MeasurementViewModel>
    {
        public MeasurementEventHandler(IRepository<MeasurementViewModel> repository) : base(repository)
        {
            Actions = new Dictionary<string, Func<IAggregateIdentity, IEvent, Task>>
            {
                {nameof(MeasurementCreated), CreateMeasurement },
                {nameof(MeasurementTypeChanged), HandleMeasurementTypeChanged },
                {nameof(MeasurementUnitChanged), HandleMeasurementUnitChanged },
                {nameof(MeasurementValueAdded), HandleMeasurementValueAdded }
            };
        }

        

        async Task CreateMeasurement(IAggregateIdentity id, IEvent @event)
        {
           if(@event is MeasurementCreated measurementCreated)
            {
                var viewModel = new MeasurementViewModel
                {
                    Id = measurementCreated.Id.Guid,
                    Type = measurementCreated.Type,
                    Unit = measurementCreated.Unit,

                };
                await Repository.AddAsync(viewModel);
            }
        }
        async Task HandleMeasurementTypeChanged(IAggregateIdentity id, IEvent @event)
        {
            if(@event is MeasurementTypeChanged measurementEvent)
            {
                var viewModel = await Repository.GetAsync(id.Guid);
                viewModel.Type = measurementEvent.Type;
                await Repository.UpdateAsync(viewModel);
            }
        }
        async Task HandleMeasurementUnitChanged(IAggregateIdentity id, IEvent @event)
        {
            if(@event is MeasurementUnitChanged measurementEvent)
            {
                var viewModel = await Repository.GetAsync(id.Guid);
                viewModel.Unit = measurementEvent.Unit;
                await Repository.UpdateAsync(viewModel);
            }
        }
        async Task HandleMeasurementValueAdded(IAggregateIdentity id, IEvent @event)
        {
            if(@event is MeasurementValueAdded measurementEvent)
            {
                var viewModel = await Repository.GetAsync(id.Guid);
                viewModel.Values.Add(measurementEvent.OccurredOn, measurementEvent.Value);
                await Repository.UpdateAsync(viewModel);
            }
        }
    }
}
