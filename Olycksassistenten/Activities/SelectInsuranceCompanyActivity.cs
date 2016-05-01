using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Olycksassistenten.Logic;

namespace Olycksassistenten.Activities
{
    [Activity(Label = "SelectInsuranceCompanyActivity")]
    public class SelectInsuranceCompanyActivity : Activity
    {
        private List<InsuranceCompany> _insuranceCompanies;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.SelectInsuranceCompany);

            InitInsuranceCompanies();

            var buttonPickInsCompanyOk = FindViewById<Button>(Resource.Id.buttonPickInsCompanyOk);
            buttonPickInsCompanyOk.Click += InsuranceCompaniesOkClick;
        }

        private void InsuranceCompaniesOkClick(object sender, System.EventArgs e)
        {
            if (ValidateInput())
            {
                var user1 = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user1"));

                var spinnerInsuranceCompany1 = FindViewById<Spinner>(Resource.Id.spinnerChooseInsuranceCompany1);
                var spinnerInsuranceCompany2 = FindViewById<Spinner>(Resource.Id.spinnerChooseInsuranceCompany2);

                var insuranceCompany1 = _insuranceCompanies.FirstOrDefault(x => x.CompanyName == spinnerInsuranceCompany1.SelectedItem.ToString());
                var insuranceCompany2 = _insuranceCompanies.FirstOrDefault(x => x.CompanyName == spinnerInsuranceCompany2.SelectedItem.ToString());

                //starta ny aktivitet och skicka vidare parametrar
                var activity = new Intent(this, typeof(RegistrationNoActivity));
                activity.PutExtra("user1", JsonConvert.SerializeObject(user1));
                activity.PutExtra("insuranceCompany1", JsonConvert.SerializeObject(insuranceCompany1));
                activity.PutExtra("insuranceCompany2", JsonConvert.SerializeObject(insuranceCompany2));
                StartActivity(activity);
            }
        }

        private void InitInsuranceCompanies()
        {
            //hämta försäkringsbolag
            var logicController = new LogicController();
            _insuranceCompanies = logicController.GetAllInsuranceCompanies();

            //Leta upp spinners
            var spinnerInsuranceCompany1 = FindViewById<Spinner>(Resource.Id.spinnerChooseInsuranceCompany1);
            var spinnerInsuranceCompany2 = FindViewById<Spinner>(Resource.Id.spinnerChooseInsuranceCompany2);

            //Skapa en adapter med innehåll
            var insuranceCompanyNames = _insuranceCompanies.Select(x => x.CompanyName).ToArray();
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, insuranceCompanyNames);

            //Tilldela innehållet till spinnern
            spinnerInsuranceCompany1.Adapter = adapter;
            spinnerInsuranceCompany2.Adapter = adapter;
        }

        private bool ValidateInput()
        {
            var spinnerInsuranceCompany1 = FindViewById<Spinner>(Resource.Id.spinnerChooseInsuranceCompany1);
            var spinnerInsuranceCompany2 = FindViewById<Spinner>(Resource.Id.spinnerChooseInsuranceCompany2);

            string message;
            if (string.IsNullOrWhiteSpace(spinnerInsuranceCompany1.SelectedItem.ToString()))
                message = "Ange försäkringsbolag, förare 1";
            else if (string.IsNullOrWhiteSpace(spinnerInsuranceCompany2.SelectedItem.ToString()))
                message = "Ange försäkringsbolag, förare 2";
            else
                return true;

            var toast = Toast.MakeText(this, message, ToastLength.Long);
            toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
            toast.Show();
            return false;
        }
    }
}