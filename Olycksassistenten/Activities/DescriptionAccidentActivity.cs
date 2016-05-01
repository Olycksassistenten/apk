using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Olycksassistenten.Logic;

namespace Olycksassistenten.Activities
{
    [Activity(Label = "DescriptionAccidentActivity")]
    public class DescriptionAccidentActivity : Activity
    {
       protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.DescriptionAccident);

            var buttonDescAccidentOk = FindViewById<Button>(Resource.Id.buttonDescAccidentOk);
            buttonDescAccidentOk.Click += AddDescription;
        }

        private void AddDescription(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                var accidentDescription = FindViewById<EditText>(Resource.Id.editTextDescAccident).Text;

                var user1 = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user1"));
                var insuranceCompany1 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany1"));
                var insuranceCompany2 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany2"));
                var vehicle1 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle1"));
                var vehicle2 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle2"));
                var pictures = Intent.GetStringArrayExtra("pictures");

                //starta ny aktivitet och skicka vidare parametrar
                var activity = new Intent(this, typeof(DataDriverActivity));
                activity.PutExtra("user1", JsonConvert.SerializeObject(user1));
                activity.PutExtra("insuranceCompany1", JsonConvert.SerializeObject(insuranceCompany1));
                activity.PutExtra("insuranceCompany2", JsonConvert.SerializeObject(insuranceCompany2));
                activity.PutExtra("vehicle1", JsonConvert.SerializeObject(vehicle1));
                activity.PutExtra("vehicle2", JsonConvert.SerializeObject(vehicle2));
                activity.PutExtra("pictures", pictures);
                activity.PutExtra("accidentDescription", accidentDescription);
                activity.PutExtra("isUser1", true);
                StartActivity(activity);
            }
        }

        private bool ValidateInput()
        {
            var accidentDescription = FindViewById<EditText>(Resource.Id.editTextDescAccident).Text;

            string message;
            if (string.IsNullOrWhiteSpace(accidentDescription))
                message = "Ange beskrivning av olyckan";
            else
                return true;

            var toast = Toast.MakeText(this, message, ToastLength.Long);
            toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
            toast.Show();
            return false;
        }
    }
}