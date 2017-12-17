using ApplicationInterfaces;
using System;

namespace PushUp.Application.Commands
{
    public sealed class SetChallengeDescription : ICommand
    {
        public SetChallengeDescription(Guid id, string description)
        {
            Id = id;
            Description = description;
        }

        public Guid Id { get; }
        public string Description { get; }
    }
}
