using ApplicationInterfaces;
using System;

namespace PushUp.Application.Commands
{
    public sealed class SetMeasurementType : ICommand
    {
        public SetMeasurementType(Guid id, string type)
        {
            Id = id;
            Type = type;
        }

        public Guid Id { get; }
        public string Type { get; }
    }
}
