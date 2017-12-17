using PushUp.Application.EventHandlers;
using StorageInterfaces;
using ViewModels;

namespace PushUp.Application.Views
{
    class ViewStoreFactory
    {
        static IViewStore viewStore = new ViewStore();
        
        internal static IViewStore ChallengeViewModelStore(IRepository<ChallengeViewModel> repository)
        {
            viewStore.TryAddEventHandler(new ChallengeEventHandler(repository));
            return viewStore;
        }

        internal static IViewStore MeasurementViewModelStore(IRepository<MeasurementViewModel> repository)
        {
            viewStore.TryAddEventHandler(new MeasurementEventHandler(repository));
            return viewStore;
        }

        internal static IViewStore WorkoutViewModelStore(IRepository<WorkoutViewModel> repository)
        {
            viewStore.TryAddEventHandler(new WorkoutEventHandler(repository));
            return viewStore;
        }
    }
}
