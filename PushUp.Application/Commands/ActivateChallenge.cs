using System;
using ApplicationInterfaces;

namespace PushUp.Application.Commands
{
    public sealed class ActivateChallenge : ICommand
    {
        public ActivateChallenge(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
