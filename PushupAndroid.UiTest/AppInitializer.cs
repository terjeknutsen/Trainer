using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace PushupAndroid.UiTest
{
    class AppInitializer
    {
        internal static AndroidApp StartApp()
        {
            return ConfigureApp.Android.PreferIdeSettings().ApkFile("C:/Users/terje knutsen/Source/Repos/PushUp/src/PushupAndroid/bin/Test/no.trainer.personal.test.apk")
                .StartApp(Xamarin.UITest.Configuration.AppDataMode.Clear);
        }
    }
}
