using ApplicationInterfaces;
using System;

namespace PushUp.Application.Commands
{
    public sealed class SetDailyRepetitions : ICommand
    {
        public SetDailyRepetitions(Guid id, int repetitions)
        {
            Id = id;
            Repetitions = repetitions;
        }

        public Guid Id { get; }
        public int Repetitions { get; }
    }
}
