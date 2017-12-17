using ApplicationInterfaces;
using System;

namespace PushUp.Application.Commands
{
    public sealed class CreateWorkout : ICommand
    {
        public CreateWorkout(Guid id,Guid challengeId, string workoutType, int repetitions)
        {
            Id = id;
            ChallengeId = challengeId;
            WorkoutType = workoutType;
            Repetitions = repetitions;
        }

        public Guid Id { get; }
        public Guid ChallengeId { get; }
        public string WorkoutType { get; }
        public int Repetitions { get; }
    }
}
