using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Olycksassistenten.Logic;

namespace Olycksassistenten.Activities
{
    [Activity(Label = "RegistrationNoActivity")]
    public class RegistrationNoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.RegistrationNo);

            InitYearSpinners();

            var buttonRegNoOk = FindViewById<Button>(Resource.Id.buttonRegNoOk);
            buttonRegNoOk.Click += RegistrationNumberOkClick;
        }

        private void RegistrationNumberOkClick(object sender, System.EventArgs e)
        {
            if (ValidateInput())
            {
                var regNo1 = FindViewById<EditText>(Resource.Id.editRegNo1).Text;
                var regNo2 = FindViewById<EditText>(Resource.Id.editRegNo2).Text;
                var model1 = FindViewById<EditText>(Resource.Id.editRegModel1).Text;
                var model2 = FindViewById<EditText>(Resource.Id.editRegModel2).Text;
                var regYear1 = int.Parse(FindViewById<Spinner>(Resource.Id.editRegYear1).SelectedItem.ToString());
                var regYear2 = int.Parse(FindViewById<Spinner>(Resource.Id.editRegYear2).SelectedItem.ToString());

                var user1 = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user1"));
                var insuranceCompany1 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany1"));
                var insuranceCompany2 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany2"));
                var vehicle1 = new Vehicle(regNo1, model1, regYear1);
                var vehicle2 = new Vehicle(regNo2, model2, regYear2);

                //starta ny aktivitet och skicka vidare parametrar
                var activity = new Intent(this, typeof(TakePhotoActivity));
                activity.PutExtra("user1", JsonConvert.SerializeObject(user1));
                activity.PutExtra("insuranceCompany1", JsonConvert.SerializeObject(insuranceCompany1));
                activity.PutExtra("insuranceCompany2", JsonConvert.SerializeObject(insuranceCompany2));
                activity.PutExtra("vehicle1", JsonConvert.SerializeObject(vehicle1));
                activity.PutExtra("vehicle2", JsonConvert.SerializeObject(vehicle2));
                StartActivity(activity);
            }
        }

        private void InitYearSpinners()
        {
            //Leta upp spinners
            var spinnerYear1 = FindViewById<Spinner>(Resource.Id.editRegYear1);
            var spinnerYear2 = FindViewById<Spinner>(Resource.Id.editRegYear2);

            //Skapa en adapter med innehåll
            var years = new[] { "1976","1977","1978","1979","1980", "1981", "1982", "1983", "1984", "1985", 
                                "1986", "1987", "1988", "1989", "1990", "1991", "1992", "1993", "1994", "1995",
                                "1996", "1997","1998", "1999","2000", "2001", "2002", "2003", "2004", "2005", 
                                "2006", "2007","2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", 
                                "2016", "2017" };

            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, years);

            //Tilldela innehållet till spinnern
            spinnerYear1.Adapter = adapter;
            spinnerYear2.Adapter = adapter;

            //nuvarande år förvald
            spinnerYear1.SetSelection(years.Length - 2);
            spinnerYear2.SetSelection(years.Length - 2);
        }

        private bool ValidateInput()
        {
            var regNo1 = FindViewById<EditText>(Resource.Id.editRegNo1).Text;
            var regNo2 = FindViewById<EditText>(Resource.Id.editRegNo2).Text;
            var model1 = FindViewById<EditText>(Resource.Id.editRegModel1).Text;
            var model2 = FindViewById<EditText>(Resource.Id.editRegModel2).Text;

            string message;
            if (string.IsNullOrWhiteSpace(regNo1))
                message = "Ange registreringsnummer, förare 1";
            else if (string.IsNullOrWhiteSpace(regNo2))
                message = "Ange registreringsnummer, förare 2";
            else if (string.IsNullOrWhiteSpace(model1))
                message = "Ange modell, förare 1";
            else if (string.IsNullOrWhiteSpace(model2))
                message = "Ange modell, förare 2";
            else
                return true;

            var toast = Toast.MakeText(this, message, ToastLength.Long);
            toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
            toast.Show();
            return false;
        }
    }
}