using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Olycksassistenten.Logic;

namespace Olycksassistenten.Activities
{
    [Activity(Label = "MainActivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ActionBar.Hide();
            SetContentView(Resource.Layout.Main);

            var buttonStartLogin = FindViewById<Button>(Resource.Id.buttonStartLogin);
            buttonStartLogin.Click += Login;

            var textViewStartRegAccount = FindViewById<TextView>(Resource.Id.textViewStartRegAccount);
            textViewStartRegAccount.Click += delegate { StartActivity(typeof(RegisterAccountActivity)); };

            var textViewStartForgotPassword = FindViewById<TextView>(Resource.Id.textViewStartForgotPassword);
            textViewStartForgotPassword.Click += delegate { StartActivity(typeof(ForgotPasswordActivity)); };
        }

        private void Login(object sender, System.EventArgs e)
        {
            if (ValidateInput())
            {
                var username = FindViewById<EditText>(Resource.Id.editStartUserName).Text;
                var password = FindViewById<EditText>(Resource.Id.editStartPassword).Text;

                var logicController = new LogicController();

                //logga in användaren
                var user1 = logicController.Login(username, password);
                if (user1 == null)
                {
                    var toast = Toast.MakeText(this, "Felaktigt användarnamn eller lösenord", ToastLength.Long);
                    toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
                    toast.Show();
                }
                else
                {
                    //starta ny aktivitet och skicka med användaren
                    var activity = new Intent(this, typeof(SelectInsuranceCompanyActivity));
                    activity.PutExtra("user1", JsonConvert.SerializeObject(user1));
                    StartActivity(activity);
                }
            }
        }

        private bool ValidateInput()
        {
            var username = FindViewById<EditText>(Resource.Id.editStartUserName).Text;
            var password = FindViewById<EditText>(Resource.Id.editStartPassword).Text;

            string message;
            if (string.IsNullOrWhiteSpace(username))
                message = "Ange användarnamn";
            else if (string.IsNullOrWhiteSpace(password))
                message = "Ange lösenord";
            else
                return true;

            var toast = Toast.MakeText(this, message, ToastLength.Long);
            toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
            toast.Show();
            return false;
        }
    }
}

