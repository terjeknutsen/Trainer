using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Android;

namespace PushupAndroid.UiTest.WhenStartup
{
    [TestFixture]
    public class GivenOneWorkoutPending
    {
        AndroidApp app;
        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp();
        }
        [Test]
        public void Then_workout_element_should_be_displayed()
        {
            app.Invoke("OneWorkoutPending","test");
            app.Flash(c => c.Id("workout_reps")); ;
            app.Flash(c => c.Id("workout_description")); ;
            app.Flash(c => c.Id("executedBtn")); ;
        }
    }
}
