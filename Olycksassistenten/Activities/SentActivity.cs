using Android.App;
using Android.OS;

namespace Olycksassistenten.Activities
{
    [Activity(Label = "SentActivity")]
    public class SentActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.Sent);
        }
    }
}