using System.Collections.Generic;
using Android.Content;
using Android.Support.V4.App;
using Java.Lang;
using no.trainer.personal.Fragments;

namespace no.trainer.personal.Adapters
{
    public sealed class PagerAdapter : FragmentPagerAdapter
    {
        Dictionary<int, FragmentHolder> Fragments;

        public PagerAdapter(Context context, FragmentManager fm):base(fm)
        {
            this.context = context;
            Fragments = new Dictionary<int, FragmentHolder>
            {
                {0,Challenge },
                {1,Measurement }
            };
        }

        public override int Count => 2;

        public override Fragment GetItem(int position)
        {
            return Fragments[position].Tab;
        }
        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return Fragments[position].Title;
        }

        FragmentHolder challenge;
        FragmentHolder measurement;
        private readonly Context context;

        private FragmentHolder Challenge => challenge ?? (challenge = new FragmentHolder
        {
            Title = new String(context.GetString(Resource.String.challenge)),
            Tab = ChallengeFragment.NewInstance()
        });

        private FragmentHolder Measurement => measurement ?? (measurement = new FragmentHolder
        {
            Title = new String(context.GetString(Resource.String.workout)),
            Tab = WorkoutsFragment.NewInstance()
        });


            class FragmentHolder
        {
            public Fragment Tab { get; set; }
            public ICharSequence Title { get; set; }
        }
    }
}