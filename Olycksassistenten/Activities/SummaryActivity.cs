using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using Olycksassistenten.Logic;

namespace Olycksassistenten.Activities
{
    [Activity(Label = "SummaryActivity")]
    public class SummaryActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.Summary);

            DisplaySummary();

            var buttonSummarySend = FindViewById<Button>(Resource.Id.buttonSummarySend);
            buttonSummarySend.Click += SendInsuranceClaims;
        }
       
        private void DisplaySummary()
        {
            var user1 = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user1"));
            var user2 = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user2"));
            var insuranceCompany1 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany1"));
            var insuranceCompany2 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany2"));
            var vehicle1 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle1"));
            var vehicle2 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle2"));
            var accidentDescription = Intent.GetStringExtra("accidentDescription");

            //visa info om förare 1
            FindViewById<TextView>(Resource.Id.textViewSummaryName1).Text = user1.FullName;
            FindViewById<TextView>(Resource.Id.textViewSummarySSN1).Text = user1.Ssn.ToString();
            FindViewById<TextView>(Resource.Id.textViewSummaryAddress1).Text = user1.Address;
            FindViewById<TextView>(Resource.Id.textViewSummaryPhone1).Text = user1.Phone;
            FindViewById<TextView>(Resource.Id.textViewSummaryMail1).Text = user1.Email;
            FindViewById<TextView>(Resource.Id.textViewSummaryRegNo1).Text = vehicle1.RegistrationNumber;
            FindViewById<TextView>(Resource.Id.textViewSummaryInsuranceCo1).Text = insuranceCompany1.CompanyName;

            //visa info om förare 2
            FindViewById<TextView>(Resource.Id.textViewSummaryName2).Text = user2.FullName;
            FindViewById<TextView>(Resource.Id.textViewSummarySSN2).Text = user2.Ssn.ToString();
            FindViewById<TextView>(Resource.Id.textViewSummaryAddress2).Text = user2.Address;
            FindViewById<TextView>(Resource.Id.textViewSummaryPhone2).Text = user2.Phone;
            FindViewById<TextView>(Resource.Id.textViewSummaryMail2).Text = user2.Email;
            FindViewById<TextView>(Resource.Id.textViewSummaryRegNo2).Text = vehicle2.RegistrationNumber;
            FindViewById<TextView>(Resource.Id.textViewSummaryInsuranceCo2).Text = insuranceCompany2.CompanyName;

            //visa beskrivning av olyckan
            FindViewById<TextView>(Resource.Id.textViewSummaryDesc).Text = accidentDescription;
        }

        private void SendInsuranceClaims(object sender, EventArgs e)
        {
            var alert = new AlertDialog.Builder(this);
            alert.SetTitle("Sänder skadeanmälan... Detta kan ta lite tid.");
            alert.SetPositiveButton("OK", OkClick);
            RunOnUiThread(() => alert.Show());
        }

        private void OkClick(object sender, DialogClickEventArgs e)
        {
            var user1 = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user1"));
            var user2 = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user2"));
            var insuranceCompany1 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany1"));
            var insuranceCompany2 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany2"));
            var vehicle1 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle1"));
            var vehicle2 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle2"));
            var pictures = Intent.GetStringArrayExtra("pictures");
            var accidentDescription = Intent.GetStringExtra("accidentDescription");

            var logicController = new LogicController();
            try
            {
                //skicka iväg anmälan
                var report = new Report(user1, user2, vehicle1, vehicle2, insuranceCompany1, insuranceCompany2, accidentDescription, pictures.ToList());
                logicController.SubmitInsuranceClaims(report);
            }
            catch (Exception)
            {
                //visa meddelande om att det inte gick att skicka anmälan
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("Misslyckades att skicka anmälan. Försök lite senare igen.");
                alert.SetPositiveButton("OK", (senderAlert, args) => StartActivity(typeof(MainActivity)));
                RunOnUiThread(() => alert.Show());
            }

            //starta ny aktivitet och skicka vidare parametrar
            var activity = new Intent(this, typeof(SentActivity));
            activity.PutExtra("user1", JsonConvert.SerializeObject(user1));
            activity.PutExtra("user2", JsonConvert.SerializeObject(user2));
            activity.PutExtra("insuranceCompany1", JsonConvert.SerializeObject(insuranceCompany1));
            activity.PutExtra("insuranceCompany2", JsonConvert.SerializeObject(insuranceCompany2));
            activity.PutExtra("vehicle1", JsonConvert.SerializeObject(vehicle1));
            activity.PutExtra("vehicle2", JsonConvert.SerializeObject(vehicle2));
            activity.PutExtra("pictures", pictures);
            activity.PutExtra("accidentDescription", accidentDescription);
            StartActivity(activity);
        }
    }
}