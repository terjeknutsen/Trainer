using ApplicationInterfaces;
using System;

namespace PushUp.Application.Commands
{
    public sealed class SetMeasurementUnit : ICommand
    {
        public SetMeasurementUnit(Guid id, string unit)
        {
            Id = id;
            Unit = unit;
        }

        public Guid Id { get; }
        public string Unit { get; }
    }
}
