using System;
using ApplicationInterfaces;

namespace PushUp.Application.Commands
{
    public sealed class PauseChallenge : ICommand
    {
        public PauseChallenge(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
