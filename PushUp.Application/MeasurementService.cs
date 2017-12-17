using CoreDomain;
using PushUp.Application.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainInterfaces;
using StorageInterfaces;
using PushUp.Application.Commands;
using CoreDomain.Identity;
using ApplicationInterfaces;
using ViewModels;
using PushUp.Application.Views;

namespace PushUp.Application
{
    public sealed class MeasurementService : ServiceBase<Measurement>
    {
        public MeasurementService(IEventStore eventStore,IRepository<MeasurementViewModel> repository) : base(eventStore)
        {
            ViewStore = ViewStoreFactory.MeasurementViewModelStore(repository);
        }

        public override async Task ExecuteCommand(ICommand cmd)
        {
           await When((dynamic)cmd);
        }
        protected override Measurement BuildAggregate(IEnumerable<IEvent> events)
        {
            return new Measurement(events);
        }
        async Task When(CreateMeasurement cmd)
        {
            var measurementId = new MeasurementId(cmd.Id);
            await Update(measurementId, m => m.Create(measurementId, cmd.Type, cmd.Unit));
        }
        async Task When(SetMeasurementType cmd)
        {
            await Update(new MeasurementId(cmd.Id), m => m.ChangeType(cmd.Type, DateTime.Now));
        }
        async Task When(SetMeasurementUnit cmd)
        {
            await Update(new MeasurementId(cmd.Id), m => m.ChangeUnit(cmd.Unit, DateTime.Now));
        }
        async Task When(AddMeasurementValue cmd)
        {
            await Update(new MeasurementId(cmd.Id), m => m.AddValue(cmd.Value, DateTime.Now));
        }

        
    }
}
