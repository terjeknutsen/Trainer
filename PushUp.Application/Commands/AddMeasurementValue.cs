using ApplicationInterfaces;
using System;

namespace PushUp.Application.Commands
{
    public sealed class AddMeasurementValue : ICommand
    {
        public AddMeasurementValue(Guid id, double value)
        {
            Id = id;
            Value = value;
        }

        public Guid Id { get; }
        public double Value { get; }
    }
}
