using System;
using ApplicationInterfaces;

namespace PushUp.Application.Commands
{
    public sealed class CreateChallenge : ICommand
    {
        public CreateChallenge(Guid id,int repetitions,string description)
        {
            Id = id;
            Repetitions = repetitions;
            Description = description;
        }
        public Guid Id { get; }
        public int Repetitions { get;  }
        public string Description { get;  }
    }
}
