using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using PushupAndroid.Adapters;
using ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using no.trainer.personal.Interfaces;
using Android.Util;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using Android.Support.V4.View;
using no.trainer.personal.Broadcasts;
using Android.Support.V4.Content;
using Android.Content;

namespace no.trainer.personal.Fragments
{
    public sealed class ChallengeFragment : Android.Support.V4.App.Fragment
    {
        Stopwatch stopwatch;

        private RecyclerView recyclerView;
        private MainAdapter adapter;
        private IGetChallenge challengeRepository;
        private IGetWorkout workoutRepository;
        private IGetMeasurements measurementRepository;
        private ISetWorkoutAsExecuted workoutExecutedSetter;
        private ISetRestingCycle restingCycleSetter;
        private IPauseChallenge challengePauser;
        private IActivateChallenge challengeActivator;
        private SetWorkoutAsExecutedSuccessReceiver setWorkoutAsExecutedSuccessReceiver;

        public static ChallengeFragment NewInstance()
        {
            return new ChallengeFragment();
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            StartStopwatch();
            return inflater.Inflate(Resource.Layout.fragment_challenge_view, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            
            base.OnViewCreated(view, savedInstanceState);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recycler_view_challenge);
            var layoutManager = new StaggeredGridLayoutManager(1, StaggeredGridLayoutManager.Vertical);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.HasFixedSize = false;
            ViewCompat.SetNestedScrollingEnabled(recyclerView, false);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            challengeRepository = Activity as IGetChallenge;

            workoutRepository = Activity as IGetWorkout;

            measurementRepository = Activity as IGetMeasurements;

            workoutExecutedSetter = Activity as ISetWorkoutAsExecuted;

            restingCycleSetter = Activity as ISetRestingCycle;

            challengePauser = Activity as IPauseChallenge;

            challengeActivator = Activity as IActivateChallenge;

            var changeEvent = Activity as INotifyPropertyChanged;

            if (challengeRepository == null)
                throw new NotImplementedException($"{Activity.Class.SimpleName} must implement {nameof(IGetChallenge)}");
            if (workoutRepository == null)
                throw new NotImplementedException($"{Activity.Class.SimpleName} must implement {nameof(IGetWorkout)}");
            if (measurementRepository == null)
                throw new NotImplementedException($"{Activity.Class.SimpleName} must implement {nameof(IGetMeasurements)}");
            if (workoutExecutedSetter == null)
                throw new NotImplementedException($"{Activity.Class.SimpleName} must implement {nameof(ISetWorkoutAsExecuted)}");
            if (restingCycleSetter == null)
                throw new NotImplementedException($"{Activity.Class.SimpleName} must implement {nameof(ISetRestingCycle)}");
            if (challengePauser == null)
                throw new NotImplementedException($"{Activity.Class.SimpleName} must implement {nameof(IPauseChallenge)}");
            if (challengeActivator == null)
                throw new NotImplementedException($"{Activity.Class.SimpleName} must implement {nameof(IActivateChallenge)}");
            if (changeEvent == null)
                throw new NotImplementedException($"{Activity.Class.SimpleName} must implement {nameof(INotifyPropertyChanged)}");

            changeEvent.PropertyChanged += delegate { Populate(); };
            //InitializeAdapter()
            adapter = new MainAdapter(
                 Resources,
                 workoutExecutedSetter.Execute,
                 restingCycleSetter.SetChallengeRestingCycle,
                 challengePauser.Pause,
                 challengeActivator.Activate);
            recyclerView.SetAdapter(adapter);

        }
       
        public override void OnStart()
        {
            base.OnStart();
        }

        public override void OnResume()
        {
            base.OnResume();
            Populate();
            LogStartupTime();
        }
        public override void OnPause()
        {
            base.OnPause();
        }
        public override void OnStop()
        {
            base.OnStop();
        }

        private Task InitializeAdapter()
        {
            return Task.Factory.StartNew(() =>
            {
                adapter = new MainAdapter(
                    Resources,
                    workoutExecutedSetter.Execute,
                    restingCycleSetter.SetChallengeRestingCycle,
                    challengePauser.Pause,
                    challengeActivator.Activate);
                recyclerView.SetAdapter(adapter);
            });

        }
        private async Task Populate()
        {
            var challenges = await challengeRepository.Challenges;
            var workouts = await workoutRepository.Workouts;
            var measurements = await measurementRepository.Measurements;
            List<IViewModel> viewModels = new List<IViewModel>(workouts);
            viewModels.AddRange(challenges);
            viewModels.AddRange(measurements);
            View.Post(()=> adapter.SetItems(viewModels));
        }

        [Conditional("TEST")]
        private void StartStopwatch()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }
        [Conditional("TEST")]
        private void LogStartupTime()
        {
            if (stopwatch.Elapsed > TimeSpan.FromMilliseconds(60))
            {
                Log.Debug("STOPWATCH", $"{Class.SimpleName} takes to long to show view!! {stopwatch.Elapsed}");
            }
            stopwatch.Stop();
            stopwatch.Reset();
        }
    }
}