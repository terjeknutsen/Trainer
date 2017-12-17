using ApplicationInterfaces;
using System;

namespace PushUp.Application.Commands
{
    public sealed class CreateMeasurement : ICommand
    {
        public CreateMeasurement(Guid id, string type, string unit)
        {
            Id = id;
            Type = type;
            Unit = unit;
        }

        public Guid Id { get; }
        public string Type { get; }
        public string Unit { get; }
    }
}
