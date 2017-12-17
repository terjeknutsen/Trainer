
using System;
using Android.App;
using TinyIoC;
using ViewModels;
using StorageInterfaces;
using PushupAndroid.TestRepository;
using Android.Runtime;
using ApplicationInterfaces;
using PushUp.Application;

namespace PushupAndroid
{
    [Application]
    public class PushupApplication : Application
    {
        protected PushupApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer) { }
        public override void OnCreate()
        {
            base.OnCreate();
            Configure();
        }

        private void Configure()
        {
            var container = TinyIoCContainer.Current;
#if TEST
            container.Register<IRepository<ChallengeViewModel>, ChallengeRepository> ();
            container.Register<IRepository<WorkoutViewModel>, WorkoutRepository>();
            container.Register<IRepository<MeasurementViewModel>, MeasurementRepository>();
#else

#endif
            container.Register<IEventStore, TestEventStore>().AsSingleton();
            container.Register<IApplicationService>(new ChallengeService(container.Resolve<IEventStore>(), container.Resolve<IRepository<ChallengeViewModel>>()), nameof(ChallengeService));
            container.Register<IApplicationService>(new WorkoutService(container.Resolve<IEventStore>(), container.Resolve<IRepository<WorkoutViewModel>>()), nameof(WorkoutService));
            container.Register<IApplicationService>(new MeasurementService(container.Resolve<IEventStore>(),container.Resolve<IRepository<MeasurementViewModel>>()),nameof(MeasurementService));
        }
     
    }

   
}