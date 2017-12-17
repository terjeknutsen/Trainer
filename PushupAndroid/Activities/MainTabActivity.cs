using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Java.Interop;
using Android.Views;
using no.trainer.personal.Interfaces;
using System.Collections.Generic;
using ViewModels;
using StorageInterfaces;
using System.Threading.Tasks;
using System.Diagnostics;
using System;
using PushupAndroid.TestRepository;
using PushUp.Application.Commands;
using Android.Content;
using no.trainer.personal.Services;
using no.trainer.personal.Broadcasts;
using Android.Support.V4.Content;
using System.Linq;
using Android.Util;

namespace no.trainer.personal.Activities
{
    [Activity(Label = "MainTabActivity", MainLauncher = true, Theme = "@style/Theme.Startup")]
    public class MainTabActivity : AppCompatActivity,
        IGetChallenge,
        IGetWorkout,
        ISetWorkoutAsExecuted,
        IPauseChallenge,
        IActivateChallenge,
        ISetRestingCycle,
        INotifyPropertyChanged,
        IGetMeasurements
    {
        Stopwatch stopwatch;
        private NavigationView navigationView;
        private ViewPager viewPager;
        private DrawerLayout drawerLayout;
        private TabLayout tabLayout;
        private Android.Support.V7.Widget.Toolbar toolbar;
        private Adapters.PagerAdapter pagerAdapter;
        private IRepository<ChallengeViewModel> challengeRepository;
        private IRepository<WorkoutViewModel> workoutRepository;
        private IRepository<MeasurementViewModel> measurementRepository;
        private SetWorkoutAsExecutedSuccessReceiver setWorkoutAsExecutedSuccessReceiver;
        private SetWorkoutAsExecutedFailureReceiver setWorkoutAsExecutedFailureReceiver;
        private CreateWorkoutSuccessReceiver createWorkoutSuccessReceiver;
        private PauseChallengeSuccessReceiver pauseChallengeSuccessReceiver;
        private PauseChallengeFailureReceiver pauseChallengeFailureReceiver;
        private ActivateChallengeSuccessReceiver activateChallengeSuccessReceiver;
        private ActivateChallengeFailureReceiver activateChallengeFailureReceiver;
        private SetRestingCycleSuccessReceiver setRestingCycleSuccessReceiver;
        private SetRestingCycleFailureReceiver setRestingCycleFailureReceiver;

        public event EventHandler PropertyChanged;
        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, EventArgs.Empty);
        }

        public Task<IEnumerable<ChallengeViewModel>> Challenges => challengeRepository.AllAsync();

        public Task<IEnumerable<WorkoutViewModel>> Workouts => workoutRepository.AllAsync();

        public Task<IEnumerable<MeasurementViewModel>> Measurements => measurementRepository.AllAsync();

        public MainTabActivity()
        {
            challengeRepository = TinyIoC.TinyIoCContainer.Current.Resolve<IRepository<ChallengeViewModel>>();
            workoutRepository = TinyIoC.TinyIoCContainer.Current.Resolve<IRepository<WorkoutViewModel>>();
            measurementRepository = TinyIoC.TinyIoCContainer.Current.Resolve <IRepository<MeasurementViewModel>>();
            challengeRepository.OnChange += delegate { OnPropertyChanged(); };
            workoutRepository.OnChange += delegate { OnPropertyChanged(); };
            measurementRepository.OnChange += delegate { OnPropertyChanged(); };
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.AppTheme_Teal);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_tab_view);
           
            navigationView = FindViewById<NavigationView>(Resource.Id.main_navigation_view);
            viewPager = FindViewById<ViewPager>(Resource.Id.main_view_pager);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.main_drawer_layout);
            tabLayout = FindViewById<TabLayout>(Resource.Id.main_tab_layout);
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.main_toolbar);
            pagerAdapter = new Adapters.PagerAdapter(this, SupportFragmentManager);
            viewPager.Adapter = pagerAdapter;
            viewPager.OffscreenPageLimit = 2;
            tabLayout.SetupWithViewPager(viewPager);
        }

        protected override void OnStart()
        {
            base.OnStart();
            StartStopwatch();
            RegisterReceivers();
        }

        private void RegisterReceivers()
        {
            Task.Run(() =>
            {
                setWorkoutAsExecutedSuccessReceiver = new SetWorkoutAsExecutedSuccessReceiver(OnExecuted);
                setWorkoutAsExecutedFailureReceiver = new SetWorkoutAsExecutedFailureReceiver(OnExecutedFailed);
                createWorkoutSuccessReceiver = new CreateWorkoutSuccessReceiver(OnWorkoutCreated);
                pauseChallengeSuccessReceiver = new PauseChallengeSuccessReceiver(OnChallengePaused);
                pauseChallengeFailureReceiver = new PauseChallengeFailureReceiver(OnChallengePauseFailed);
                activateChallengeSuccessReceiver = new ActivateChallengeSuccessReceiver(OnChallengeActivated);
                activateChallengeFailureReceiver = new ActivateChallengeFailureReceiver(OnChallengeActivatedFailed);
                setRestingCycleSuccessReceiver = new SetRestingCycleSuccessReceiver(OnRestingCycleSet);
                setRestingCycleFailureReceiver = new SetRestingCycleFailureReceiver(OnSetRestingCycleFailed);
                LocalBroadcastManager.GetInstance(this).RegisterReceiver(setWorkoutAsExecutedSuccessReceiver, new IntentFilter(nameof(SetWorkoutAsExecutedSuccessReceiver)));
                LocalBroadcastManager.GetInstance(this).RegisterReceiver(setWorkoutAsExecutedFailureReceiver, new IntentFilter(nameof(SetWorkoutAsExecutedFailureReceiver)));
                LocalBroadcastManager.GetInstance(this).RegisterReceiver(createWorkoutSuccessReceiver, new IntentFilter(nameof(CreateWorkoutSuccessReceiver)));
                LocalBroadcastManager.GetInstance(this).RegisterReceiver(pauseChallengeSuccessReceiver, new IntentFilter(nameof(PauseChallengeSuccessReceiver)));
                LocalBroadcastManager.GetInstance(this).RegisterReceiver(pauseChallengeFailureReceiver, new IntentFilter(nameof(PauseChallengeFailureReceiver)));
                LocalBroadcastManager.GetInstance(this).RegisterReceiver(activateChallengeSuccessReceiver, new IntentFilter(nameof(ActivateChallengeSuccessReceiver)));
                LocalBroadcastManager.GetInstance(this).RegisterReceiver(activateChallengeFailureReceiver, new IntentFilter(nameof(ActivateChallengeFailureReceiver)));
                LocalBroadcastManager.GetInstance(this).RegisterReceiver(setRestingCycleSuccessReceiver, new IntentFilter(nameof(SetRestingCycleSuccessReceiver)));
                LocalBroadcastManager.GetInstance(this).RegisterReceiver(setRestingCycleFailureReceiver, new IntentFilter(nameof(SetRestingCycleFailureReceiver)));
        });

        }



        protected override void OnResume()
        {
            base.OnResume();
            LogStartupTime();
            OneChallengeActive();
            OneWorkoutPending("");
            UseTwoMeasurements();

        }
        protected override void OnStop()
        {
            base.OnStop();
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(setWorkoutAsExecutedSuccessReceiver);
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(setWorkoutAsExecutedFailureReceiver);
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(createWorkoutSuccessReceiver);
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(pauseChallengeSuccessReceiver);
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(pauseChallengeFailureReceiver);
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(activateChallengeSuccessReceiver);
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(activateChallengeFailureReceiver);
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(setRestingCycleSuccessReceiver);
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(setRestingCycleFailureReceiver);
        }

        public void Execute(Guid id)
        {
            var intent = new Intent(this, typeof(SetWorkoutExecutedService));
            var bundle = new Bundle();
            bundle.PutString(nameof(SetWorkoutAsExecuted.Id), id.ToString());
            intent.PutExtra(nameof(SetWorkoutAsExecuted), bundle);

            StartService(intent);
        }
        public void Pause(Guid id)
        {
            var intent = new Intent(this, typeof(PauseChallengeService));
            var bundle = new Bundle();
            bundle.PutString(nameof(PauseChallenge.Id), id.ToString());
            intent.PutExtra(nameof(PauseChallenge), bundle);
            StartService(intent);
        }

        public void Activate(Guid id)
        {
            var intent = new Intent(this, typeof(ActivateChallengeService));
            var bundle = new Bundle();
            bundle.PutString(nameof(ActivateChallenge.Id), id.ToString());
            intent.PutExtra(nameof(ActivateChallenge), bundle);
            StartService(intent);
        }

        public void SetChallengeRestingCycle(Guid id,Dictionary<int, bool> cycle)
        {
            var intent = new Intent(this, typeof(SetChallengeRestingCycleService));
            var bundle = new Bundle();
            bundle.PutString(nameof(SetRestingCycle.Id), id.ToString());
            var boolArray = cycle.Values.ToArray();
            bundle.PutBooleanArray(nameof(SetRestingCycle.RestingCycle), boolArray);
            intent.PutExtra(nameof(SetRestingCycle), bundle);
            StartService(intent);
        }

        [Export("ChallengeSelected")]
        public void ChallengeSelected(IMenuItem item)
        {
            drawerLayout.CloseDrawers();
        }
        [Export("WorkoutSelected")]
        public void WorkoutSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Workout selected", ToastLength.Long).Show();
            drawerLayout.CloseDrawers();
        }
        [Export("MeasurementSelected")]
        public void MeasurementSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Measurement selected", ToastLength.Long).Show();
            drawerLayout.CloseDrawers();
        }

        private void OnSetRestingCycleFailed(Guid obj)
        {
            Toast.MakeText(this, "could not change resting cycle", ToastLength.Short).Show();
        }

        private void OnRestingCycleSet(Guid obj)
        {
            Toast.MakeText(this, "resting cycle changed", ToastLength.Short).Show();
        }

        private void OnChallengeActivatedFailed(Guid obj)
        {
            Toast.MakeText(this, "Challenge not activated", ToastLength.Short).Show();
        }

        private void OnChallengeActivated(Guid obj)
        {
            Toast.MakeText(this, "Challenge activated", ToastLength.Short).Show();
        }

        private void OnChallengePauseFailed(Guid obj)
        {
            Toast.MakeText(this, "Failed to pause challenge", ToastLength.Short).Show();
        }

        private void OnChallengePaused(Guid obj)
        {
            Toast.MakeText(this, "Challenge paused", ToastLength.Short).Show();
        }

        private void OnWorkoutCreated(Guid obj)
        {
            Toast.MakeText(this, "Workout created", ToastLength.Short).Show();
        }

        private void OnExecutedFailed(Guid obj)
        {
            Toast.MakeText(this, "Execute failed", ToastLength.Short).Show();
        }

        private void OnExecuted(Guid obj)
        {
            Toast.MakeText(this, "Executed", ToastLength.Short).Show();
        }


        [Conditional("TEST")]
        [Export("OneChallengeActive")]
        public void OneChallengeActive()
        {
            var eventStore = TinyIoC.TinyIoCContainer.Current.Resolve<IEventStore>();
            TestData.UseSingleChallenge(eventStore);
        }
        
        [Conditional("TEST")]
        [Export("OneWorkoutPending")]
        public void OneWorkoutPending(string test)
        {

            var eventStore = TinyIoC.TinyIoCContainer.Current.Resolve<IEventStore>();
            TestData.BuildPendingWorkout(eventStore);
        }
        [Conditional("TEST")]
        [Export("UseTwoMeasurements")]
        public void UseTwoMeasurements()
        {
            var eventStore = TinyIoC.TinyIoCContainer.Current.Resolve<IEventStore>();
            TestData.UseTwoMeasurements(eventStore);
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