using System;
using ApplicationInterfaces;

namespace PushUp.Application.Commands
{
    public sealed class CreateChallenge : ICommand
    {
        public CreateChallenge(Guid id,int repetitions,string description)
        {
            Id = id;
            DailyRepetitions = repetitions;
            Description = description;
        }
        public Guid Id { get; }
        public int DailyRepetitions { get;  }
        public string Description { get;  }
    }
}
