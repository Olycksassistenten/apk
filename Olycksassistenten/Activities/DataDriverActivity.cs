using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Olycksassistenten.Helpers;
using Olycksassistenten.Logic;
using ZXing.Mobile;

namespace Olycksassistenten.Activities
{
    [Activity(Label = "DataDriverActivity")]
    public class DataDriverActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.DataDriver);

            var buttonDataDriver1Scan = FindViewById<Button>(Resource.Id.buttonDataDriver1Scan);
            buttonDataDriver1Scan.Click += ScanDriversLicense;

            var buttonDataDriver1Ok = FindViewById<Button>(Resource.Id.buttonDataDriver1Ok);
            buttonDataDriver1Ok.Click += ButtonOkClick;

            var textViewHeader = FindViewById<TextView>(Resource.Id.textViewUppgifterFörare);
            if (Intent.GetBooleanExtra("isUser1", false))
            {
                //förare 1
                var user1 = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user1"));
                var vehicle1 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle1"));
                var insuranceCompany1 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany1"));

                textViewHeader.Text = "Uppgifter förare 1 :";
                FindViewById<TextView>(Resource.Id.textViewDataName1).Text = user1.FullName;
                FindViewById<TextView>(Resource.Id.textViewDataSSN1).Text = user1.Ssn.ToString();
                FindViewById<TextView>(Resource.Id.textViewDataAddressNo1).Text = user1.Address;
                FindViewById<TextView>(Resource.Id.textViewDataPhoneNo1).Text = user1.Phone;
                FindViewById<TextView>(Resource.Id.textViewDataMail1).Text = user1.Email;
                FindViewById<TextView>(Resource.Id.textViewDataRegNo1).Text = vehicle1.RegistrationNumber;
                FindViewById<TextView>(Resource.Id.textViewDataInsuranceCo1).Text = insuranceCompany1.CompanyName;

                //behöver inte mata in kontaktinfo
                FindViewById<TextView>(Resource.Id.textViewDataPhoneNo1).Visibility = ViewStates.Visible;
                FindViewById<TextView>(Resource.Id.textViewDataMail1).Visibility = ViewStates.Visible;
                FindViewById<LinearLayout>(Resource.Id.linearLayoutDataPhoneNo).Visibility = ViewStates.Gone;
                FindViewById<LinearLayout>(Resource.Id.linearLayoutDataMail).Visibility = ViewStates.Gone;

                //skanning ej möjlig
                buttonDataDriver1Scan.Enabled = false;
                buttonDataDriver1Ok.Enabled = true;
            }
            else
            {
                //förare 2
                textViewHeader.Text = "Uppgifter förare 2 :";

                //behöver mata in kontaktinfo
                FindViewById<TextView>(Resource.Id.textViewDataPhoneNo1).Visibility = ViewStates.Gone;
                FindViewById<TextView>(Resource.Id.textViewDataMail1).Visibility = ViewStates.Gone;
                FindViewById<EditText>(Resource.Id.editTextDataPhoneNo1).Visibility = ViewStates.Visible;
                FindViewById<EditText>(Resource.Id.editTextDataMail1).Visibility = ViewStates.Visible;

                //skanning möjlig
                buttonDataDriver1Scan.Enabled = true;
                buttonDataDriver1Ok.Enabled = false;
            }
        }

        private async void ScanDriversLicense(object sender, EventArgs e)
        {
            var scanner = new MobileBarcodeScanner { UseCustomOverlay = false, TopText = "Skannar efter körkort" };

            var result = await scanner.Scan();
            DisplayDriverData(result);

            var buttonDataDriver1Ok = FindViewById<Button>(Resource.Id.buttonDataDriver1Ok);
            buttonDataDriver1Ok.Enabled = true;
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            Intent activity;
            if (Intent.GetBooleanExtra("isUser1", false)) //förare 1
            {
                activity = new Intent(this, typeof(DataDriverActivity));
            }
            else //förare 2
            {
                if (!ValidateInput()) return;

                var fullName = FindViewById<TextView>(Resource.Id.textViewDataName1).Text;
                var ssn = long.Parse(FindViewById<TextView>(Resource.Id.textViewDataSSN1).Text);
                var address = FindViewById<TextView>(Resource.Id.textViewDataAddressNo1).Text;
                var phone = FindViewById<EditText>(Resource.Id.editTextDataPhoneNo1).Text;
                var email = FindViewById<EditText>(Resource.Id.editTextDataMail1).Text;

                var user2 = new User { FullName = fullName, Ssn = ssn, Address = address, Phone = phone, Email = email };
                activity = new Intent(this, typeof(SummaryActivity));
                activity.PutExtra("user2", JsonConvert.SerializeObject(user2));
            }

            var user1 = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user1"));
            var insuranceCompany1 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany1"));
            var insuranceCompany2 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany2"));
            var vehicle1 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle1"));
            var vehicle2 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle2"));
            var pictures = Intent.GetStringArrayExtra("pictures");
            var accidentDescription = Intent.GetStringExtra("accidentDescription");

            //starta ny aktivitet och skicka vidare parametrar
            activity.PutExtra("user1", JsonConvert.SerializeObject(user1));
            activity.PutExtra("insuranceCompany1", JsonConvert.SerializeObject(insuranceCompany1));
            activity.PutExtra("insuranceCompany2", JsonConvert.SerializeObject(insuranceCompany2));
            activity.PutExtra("vehicle1", JsonConvert.SerializeObject(vehicle1));
            activity.PutExtra("vehicle2", JsonConvert.SerializeObject(vehicle2));
            activity.PutExtra("pictures", pictures);
            activity.PutExtra("accidentDescription", accidentDescription);
            StartActivity(activity);
        }

        private void DisplayDriverData(ZXing.Result result)
        {
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                //visa scannat personnummer
                var toast = Toast.MakeText(this, string.Format("Personnummer: {0}", result.Text), ToastLength.Long);
                toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
                toast.Show();

                //slå upp förare 2 utifrån personnummer
                var logicController = new LogicController();
                var user2 = logicController.Lookup(result.Text);

                //visa info om förare 2
                var vehicle2 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle2"));
                var insuranceCompany2 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany2"));
                FindViewById<TextView>(Resource.Id.textViewDataName1).Text = user2 != null ? user2.FullName : "";
                FindViewById<TextView>(Resource.Id.textViewDataSSN1).Text = result.Text;
                FindViewById<TextView>(Resource.Id.textViewDataAddressNo1).Text = user2 != null ? user2.Address : "";
                FindViewById<TextView>(Resource.Id.textViewDataRegNo1).Text = vehicle2.RegistrationNumber;
                FindViewById<TextView>(Resource.Id.textViewDataInsuranceCo1).Text = insuranceCompany2.CompanyName;
            }
        }

        private bool ValidateInput()
        {
            var fullName = FindViewById<TextView>(Resource.Id.textViewDataName1).Text;
            var ssn = FindViewById<TextView>(Resource.Id.textViewDataSSN1).Text;
            var address = FindViewById<TextView>(Resource.Id.textViewDataAddressNo1).Text;
            var phone = FindViewById<EditText>(Resource.Id.editTextDataPhoneNo1).Text;
            var email = FindViewById<EditText>(Resource.Id.editTextDataMail1).Text;
            long ssNo;
            long.TryParse(ssn, out ssNo);

            string message;
            if (string.IsNullOrWhiteSpace(fullName))
                message = "Ange namn";
            else if (string.IsNullOrWhiteSpace(ssn) || ssn.Length != 10 || ssNo == 0)
                message = "Ange personnummer, 10 siffror";
            else if (string.IsNullOrWhiteSpace(address))
                message = "Ange address";
            else if (string.IsNullOrWhiteSpace(phone))
                message = "Ange telefon";
            else if (!MailHelper.IsValid(email))
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