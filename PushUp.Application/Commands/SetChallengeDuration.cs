using System;
using ApplicationInterfaces;

namespace PushUp.Application.Commands
{
    public sealed class SetChallengeDuration : ICommand
    {
        public SetChallengeDuration(Guid id, TimeSpan duration)
        {
            Id = id;
            Duration = duration;
        }

        public Guid Id { get; }
        public TimeSpan Duration { get; }
    }
}
