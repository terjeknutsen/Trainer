using System;

using Android.App;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace no.trainer.personal.Activities.Utils
{
    sealed class DrawerToggle : ActionBarDrawerToggle
    {
        public DrawerToggle(Activity activity, DrawerLayout drawerLayout, Android.Support.V7.Widget.Toolbar toolbar, int openDrawerContentDescRes, int closeDrawerContentDescRes) : base(activity, drawerLayout, toolbar, openDrawerContentDescRes, closeDrawerContentDescRes)
        {}
        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            var handler = DrawerClosed;
            handler?.Invoke(this, EventArgs.Empty);
        }
        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
            var handler = DrawerOpened;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler DrawerClosed;
        public event EventHandler DrawerOpened;
    }
}