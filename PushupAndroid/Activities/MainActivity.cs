using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using PushupAndroid.Adapters;
using System;
using StorageInterfaces;
using ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using PushupAndroid.TestRepository;
using Android;
using no.trainer.personal.Services;
using Android.Content;
using no.trainer.personal.Broadcasts;
using Android.Support.V4.Content;
using PushUp.Application.Commands;
using Android.Support.V4.Widget;
using no.trainer.personal.Activities.Utils;
using Android.Views;
using Android.Support.Design.Widget;
using Resource = no.trainer.personal.Resource;
using Android.Content.Res;
using Java.Interop;

namespace PushupAndroid.Activities
{
    [Activity(Label = "PushupAndroid",WindowSoftInputMode = Android.Views.SoftInput.StateHidden)]
    public class MainActivity : AppCompatActivity
    {
        private RecyclerView recyclerView;
        private MainAdapter adapter;
        private IRepository<ChallengeViewModel> challengeRepository;
        private IRepository<WorkoutViewModel> workoutRepository;

        public MainActivity()
        {
            var container = TinyIoC.TinyIoCContainer.Current;
            challengeRepository = container.Resolve<IRepository<ChallengeViewModel>>();
            workoutRepository = container.Resolve<IRepository<WorkoutViewModel>>();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.main);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.main_toolbar);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.main_drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.main_navigation_view);
            drawerToggle = new DrawerToggle(this, drawerLayout, toolbar, Resource.String.executed, Resource.String.measurement);
            drawerToggle.DrawerOpened += OnDrawerOpened;
            drawerToggle.DrawerClosed += OnDrawerClosed;
            drawerLayout.AddDrawerListener(drawerToggle);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerview);
            var layoutManager = new StaggeredGridLayoutManager(1, StaggeredGridLayoutManager.Vertical);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.HasFixedSize = false;
            adapter = new MainAdapter(Resources, SetAsExecuted, (a,i) => { }, (a) => { }, (a) => { });
            recyclerView.SetAdapter(adapter);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            }
        }

        private void OnDrawerClosed(object sender, EventArgs e)
        {
            SupportActionBar.Title = GetString(Resource.String.challenge);
            InvalidateOptionsMenu();
        }

        private void OnDrawerOpened(object sender, EventArgs e)
        {
            SupportActionBar.Title = GetString(Resource.String.open_menu);
            InvalidateOptionsMenu();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            drawerToggle.OnConfigurationChanged(newConfig);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            drawerToggle.SyncState();
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            navigationView.SetCheckedItem(Resource.Id.nav_challenge);
            return drawerToggle.OnOptionsItemSelected(item);
        }

        readonly string[] permissions = { Manifest.Permission.Internet };
        private SetWorkoutAsExecutedSuccessReceiver setWorkoutAsExecutedSuccessReceiver;
        private SetWorkoutAsExecutedFailureReceiver setWorkoutAsExecutedFailureReceiver;
        private CreateWorkoutSuccessReceiver createWorkoutSuccessReceiver;
        private DrawerLayout drawerLayout;
        private NavigationView navigationView;
        private DrawerToggle drawerToggle;

        protected override void OnStart()
        {
            base.OnStart();
            setWorkoutAsExecutedSuccessReceiver = new SetWorkoutAsExecutedSuccessReceiver(OnExecuted);
            setWorkoutAsExecutedFailureReceiver = new SetWorkoutAsExecutedFailureReceiver(OnExecutedFailed);
            createWorkoutSuccessReceiver = new CreateWorkoutSuccessReceiver(OnWorkoutCreated);
            LocalBroadcastManager.GetInstance(this).RegisterReceiver(setWorkoutAsExecutedSuccessReceiver, new IntentFilter(nameof(SetWorkoutAsExecutedSuccessReceiver)));
            LocalBroadcastManager.GetInstance(this).RegisterReceiver(setWorkoutAsExecutedFailureReceiver, new IntentFilter(nameof(SetWorkoutAsExecutedFailureReceiver)));
            LocalBroadcastManager.GetInstance(this).RegisterReceiver(createWorkoutSuccessReceiver, new IntentFilter(nameof(CreateWorkoutSuccessReceiver)));

        }
        protected override void OnStop()
        {
            base.OnStop();
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(setWorkoutAsExecutedSuccessReceiver);
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(setWorkoutAsExecutedFailureReceiver);
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(createWorkoutSuccessReceiver);
        }

        protected override void OnResume()
        {
            base.OnResume();

            OneChallengeActive();
            //Populate();
        }

        private async Task Populate()
        {
            IEnumerable<IViewModel> challenges = await challengeRepository.AllAsync();
            List<IViewModel> views = new List<IViewModel>();
            views.AddRange(challenges);
            
            RunOnUiThread(()=> adapter.SetItems(views)); 
        }

        void OnWorkoutCreated(Guid guid)
        {
            Populate();
        }

        void SetAsExecuted(Guid guid)
        {
            var intent = new Intent(this,typeof(SetWorkoutExecutedService));
            var bundle = new Bundle();
            bundle.PutString(nameof(SetWorkoutAsExecuted.Id), guid.ToString());
            intent.PutExtra(nameof(SetWorkoutAsExecuted), bundle);
            StartService(intent);
        }
        void OnExecuted(Guid id)
        {
            Populate();
        }
        void OnExecutedFailed(Guid id)
        {
            Toast.MakeText(this, "Failure when execute workout", ToastLength.Long).Show();
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

        [Conditional("TEST")]
        [Java.Interop.Export("OneWorkoutPending")]
        public void OneWorkoutPending(string test)
        {
            var eventStore = TinyIoC.TinyIoCContainer.Current.Resolve<IEventStore>();
            TestData.BuildPendingWorkout(eventStore);
            Populate();
        }
        [Conditional("TEST")]
        [Java.Interop.Export("OneChallengeActive")]
        public void OneChallengeActive()
        {
            var eventStore = TinyIoC.TinyIoCContainer.Current.Resolve<IEventStore>();
            TestData.UseSingleChallenge(eventStore);
            Populate();
        }


    }
}

