using Android.Views;
using Android.Support.V7.Widget;
using ViewModels;
using Android.Widget;
using no.trainer.personal;
using System.Linq;

namespace PushupAndroid.Adapters
{
    class MeasurementItemHolder : RecyclerView.ViewHolder
    {
        public MeasurementItemHolder(View itemView) : base(itemView)
        {}

        public MeasurementViewModel Model { get; private set; }

        public void SetViewModel(MeasurementViewModel model)
        {
            Model = model;
            ItemView.FindViewById<TextView>(Resource.Id.measurement_title).Text = model.Type;
            if(model.Values.Any())
            ItemView.FindViewById<EditText>(Resource.Id.measurement_current_value).Text = $"{model.Values.Last().Value}";
        }

    }
}