using ApplicationInterfaces;
using System;

namespace PushUp.Application.Commands
{
    public sealed class SetWorkoutAsExecuted : ICommand
    {
        public SetWorkoutAsExecuted(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
