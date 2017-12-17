using CoreDomain.Events;
using CoreDomain.Identity;
using CoreDomain.Types;
using Domain.Identity;
using DomainInterfaces;
using StorageInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace PushupAndroid.TestRepository
{
    public static class TestData
    {
        public static IList<WorkoutViewModel> WorkoutViewModels { get; private set; } = new List<WorkoutViewModel>();
        public static IList<ChallengeViewModel> ChallengeViewModels { get; private set; } = new List<ChallengeViewModel>();

        public static IList<MeasurementViewModel> MeasurementViewModels { get; private set; } = new List<MeasurementViewModel>();

        public static ChallengeId ChallengeId = new ChallengeId(Guid.NewGuid());

        public static void BuildPendingWorkout(IEventStore eventStore)
        {
            if (WorkoutViewModels.Any())
                return;
            var id = new WorkoutId(Guid.NewGuid());

            var workoutType = "Pushups";
            var createEvent = new WorkoutCreated(id, ChallengeId, new WorkoutType(workoutType), 20, DateTime.Now.AddMinutes(-13));
            var viewModel = new WorkoutViewModel
            {
                Id = id.Guid,
                Reps = 20,
                WorkoutType = workoutType
            };

            eventStore.Add(id, new List<IEvent> { createEvent });
            WorkoutViewModels.Add(viewModel);
        }

        public static void UseSingleChallenge(IEventStore eventStore)
        {
            if (ChallengeViewModels.Any())
                return;
            var id = ChallengeId;
            var description = "Hundre pushups hver dag";
            var createEvent = new ChallengeCreated(id, 100, description, DateTime.Now.AddDays(-5));
            var viewModel = new ChallengeViewModel
            {
                Id = id.Guid,
                Desciption = description,
                DailyRepetitions = 100,
                StartDate = DateTime.Now.AddDays(-5),
                ActiveMonday = true,
                ActiveTuesday = true,
                ActiveWednesday = true,
                ActiveThursday = true,
                ActiveFriday = true,
                ActiveSaturday = true,
                ActiveSunday = true,
                Duration = TimeSpan.FromDays(30),
                TotalRepetitions = 460
            };
            eventStore.Add(id, new List<IEvent> { createEvent });
            ChallengeViewModels.Add(viewModel);
        }

        public static void UseTwoMeasurements(IEventStore eventStore)
        {
            if (MeasurementViewModels.Any())
                return;
            var measurementId = new MeasurementId(Guid.NewGuid());
            var type = "Stomach";
            var unit = "cm";
            var createEvent_1 = new MeasurementCreated(measurementId, type, unit, DateTime.Now.AddDays(-3));
            var viewModel_1 = new MeasurementViewModel
            {
                Id = measurementId.Guid,
                Type = type,
                Unit = unit,
                Values = new Dictionary<DateTime, double>
                {
                    {DateTime.Now.AddDays(-3), 104.5 },
                    {DateTime.Now.AddDays(-1), 104 },
                }
            };
            var measurementId_2 = new MeasurementId(Guid.NewGuid());
            var type_2 = "High jump";
            var unit_2 = "meter";
            var createEvent_2 = new MeasurementCreated(measurementId_2, type_2, unit, DateTime.Now.AddDays(-4));
            var viewModel_2 = new MeasurementViewModel
            {
                Id = measurementId_2.Guid,
                Type = type_2,
                Unit = unit_2,
                Values = new Dictionary<DateTime, double>
                {
                    {DateTime.Now.AddDays(-4), 0.34 },
                    {DateTime.Now.AddHours(-2), 0.357 }
                }
            };
            eventStore.Add(measurementId, new List<IEvent> { createEvent_1 });
            eventStore.Add(measurementId_2, new List<IEvent> { createEvent_2 });
            MeasurementViewModels.Add(viewModel_1);
            MeasurementViewModels.Add(viewModel_2);
        }
    }
}