using Android.OS;
using Android.Views;
using Android.Widget;

namespace no.trainer.personal.Fragments
{
    public sealed class WorkoutsFragment : Android.Support.V4.App.Fragment
    {
        public static WorkoutsFragment NewInstance()
        {
            return new WorkoutsFragment();
        }

        public void HandleMeasurementTypeSelected(int index)
        {
            //unitView.Text = measurements[index].Unit;
        }


        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return new TextView(Activity) { Text = "workouts" };
        }
    //    public override void OnViewCreated(View view, Bundle savedInstanceState)
    //    {
    //        //measurementTypeSpinner = view.FindViewById<Spinner>(Resource.Id.spinner_measurement_type);
            
    //    }
    //    public override void OnActivityCreated(Bundle savedInstanceState)
    //    {
    //        base.OnActivityCreated(savedInstanceState);
    //        measurementCollection = Activity as IGetMeasurements;
    //        if (measurementCollection == null)
    //            throw new NotImplementedException($"{Activity.Class.SimpleName} must implement {nameof(IGetMeasurements)}");

    //        SetAdapters().ContinueWith(_=> OnAdapterSet(),CancellationToken.None,TaskContinuationOptions.None,TaskScheduler.FromCurrentSynchronizationContext());
    //    }

    //    private async Task SetAdapters()
    //    {
    //        measurements.AddRange(await measurementCollection.Measurements);
                           
    //           typeAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem,
    //                measurements.Select(v => v.Type).ToArray());
    //            typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
    //    }
    //    private void OnAdapterSet()
    //    {
    //        measurementTypeSpinner.Adapter = typeAdapter;
    //        measurementTypeSpinner.OnItemSelectedListener = new MeasurementTypeSelectedListener(this);
    //    }
    //}

    //class MeasurementTypeSelectedListener : Java.Lang.Object, AdapterView.IOnItemSelectedListener
    //{
    //    private readonly IHandleMeasurementTypeSelected handler;

    //    public MeasurementTypeSelectedListener(IHandleMeasurementTypeSelected handler)
    //    {
    //        this.handler = handler;
    //    }
    //    public void OnItemSelected(AdapterView parent, View view, int position, long id)
    //    {
    //        handler.HandleMeasurementTypeSelected(position);
    //    }

    //    public void OnNothingSelected(AdapterView parent)
    //    {}
    //}

    //interface IHandleMeasurementTypeSelected
    //{
    //    void HandleMeasurementTypeSelected(int index);
    }
}