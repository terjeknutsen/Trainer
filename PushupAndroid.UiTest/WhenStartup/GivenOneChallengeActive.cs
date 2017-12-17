using NUnit.Framework;
using Xamarin.UITest.Android;

namespace PushupAndroid.UiTest.WhenStartup
{
    [TestFixture]
    public class GivenOneChallengeActive
    {
        AndroidApp app;
        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp();
        }
        [Test]
        public void Then_challenge_elements_should_show()
        {
            app.Invoke("OneChallengeActive");
            app.Flash(c => c.Marked("day 5"));
            app.Flash(c => c.Marked("Hundre pushups hver dag"));
            //app.Flash(c => c.Marked("100"));
            //app.Flash(c => c.Marked("420"));
            //app.Flash(c => c.Marked("mon"));
            //app.Flash(c => c.Marked("tue"));
            //app.Flash(c => c.Marked("wed"));
            //app.Flash(c => c.Marked("thu"));
            //app.Flash(c => c.Marked("fri"));
            //app.Flash(c => c.Marked("sat"));
            //app.Flash(c => c.Marked("sun"));
            //app.Flash(c => c.Marked("mage"));
            //app.Flash(c => c.Marked("96cm"));
            //app.Flash(c => c.Marked("8cm"));
            app.Repl();
        }
    }
}
