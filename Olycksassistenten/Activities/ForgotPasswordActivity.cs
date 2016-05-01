using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Olycksassistenten.Helpers;
using Olycksassistenten.Logic;

namespace Olycksassistenten.Activities
{
    [Activity(Label = "ForgotPasswordActivity")]
    public class ForgotPasswordActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.Forgotpassword);

            var buttonForgSend = FindViewById<Button>(Resource.Id.buttonForgSend);
            buttonForgSend.Click += ForgotPassword;
        }

        private void ForgotPassword(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    var email = FindViewById<EditText>(Resource.Id.editForgMail).Text;
                    var logicController = new LogicController();
                    
                    if (logicController.ForgotPassword(email))
                    {
                        //visa meddelande om skickat lösenord
                        var toast = Toast.MakeText(this, string.Format("Lösenord skickat till: {0}", email), ToastLength.Long);
                        toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
                        toast.Show();

                        //gå tillbaka till huvudfönstret
                        StartActivity(typeof(MainActivity));
                    }
                    else
                    {
                        //visa meddelande om att e-postadressen saknas
                        var toast = Toast.MakeText(this, string.Format("E-postadressen saknas: {0}", email), ToastLength.Long);
                        toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
                        toast.Show();
                    }
                }
                catch (Exception ex)
                {
                    //visa meddelande om att det inte gick att skicka lösenord
                    var alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Misslyckades att skicka lösenord. Försök igen senare.");
                    alert.SetPositiveButton("OK", OkClick);
                    RunOnUiThread(() => alert.Show());
                }
            }
        }

        private void OkClick(object sender, DialogClickEventArgs e)
        {
            StartActivity(typeof (MainActivity));
        }

        private bool ValidateInput()
        {
            var email = FindViewById<EditText>(Resource.Id.editForgMail).Text;

            string message;
            if (!MailHelper.IsValid(email))
                message = "E-post saknas eller ogiltig";
            else
                return true;

            var toast = Toast.MakeText(this, message, ToastLength.Long);
            toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
            toast.Show();
            return false;
        }
    }
}