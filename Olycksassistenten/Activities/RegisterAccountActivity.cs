using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Olycksassistenten.Helpers;
using Olycksassistenten.Logic;

namespace Olycksassistenten.Activities
{
    [Activity(Label = "RegisterAccountActivity")]
    public class RegisterAccountActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.Registeraccount);

            var buttonRegCreate = FindViewById<Button>(Resource.Id.buttonRegCreate);
            buttonRegCreate.Click += CreateAccount;
        }

        private void CreateAccount(object sender, System.EventArgs e)
        {
            if (ValidateInput())
            {
                //hämta inmatade värden
                var username = FindViewById<EditText>(Resource.Id.editTextRegUserName).Text;
                var password = FindViewById<EditText>(Resource.Id.editTextRegPassword).Text;
                var fullName = FindViewById<EditText>(Resource.Id.editTextRegName).Text;
                var ssn = long.Parse(FindViewById<EditText>(Resource.Id.editTextRegSSN).Text);
                var address = FindViewById<EditText>(Resource.Id.editTextRegAddress).Text;
                var phone = FindViewById<EditText>(Resource.Id.editTextRegPhone).Text;
                var email = FindViewById<EditText>(Resource.Id.editTextRegMail).Text;

                //spara användaren
                var user = new User { Username = username, Password = password, FullName = fullName, Ssn = ssn, Address = address, Phone = phone, Email = email };
                var logicController = new LogicController();
                logicController.InsertOrUpdate(user);

                //gå tillbaka till huvudfönstret
                StartActivity(typeof(MainActivity));
            }
        }

        private bool ValidateInput()
        {
            var username = FindViewById<EditText>(Resource.Id.editTextRegUserName).Text;
            var password = FindViewById<EditText>(Resource.Id.editTextRegPassword).Text;
            var fullName = FindViewById<EditText>(Resource.Id.editTextRegName).Text;
            var ssn = FindViewById<EditText>(Resource.Id.editTextRegSSN).Text;
            var address = FindViewById<EditText>(Resource.Id.editTextRegAddress).Text;
            var phone = FindViewById<EditText>(Resource.Id.editTextRegPhone).Text;
            var email = FindViewById<EditText>(Resource.Id.editTextRegMail).Text;
            long ssNo;
            long.TryParse(ssn, out ssNo);

            var logicController = new LogicController();

            string message;
            if (string.IsNullOrWhiteSpace(username))
                message = "Ange användarnamn";
            else if (logicController.IsUsernameTaken(username))
                message = "Användarnamnet är upptaget. Välj annat användarnamn";
            else if (string.IsNullOrWhiteSpace(password))
                message = "Ange lösenord";
            else if (string.IsNullOrWhiteSpace(fullName))
                message = "Ange namn";
            else if (string.IsNullOrWhiteSpace(ssn) || ssn.Length != 10 || ssNo == 0)
                message = "Ange personnummer, 10 siffror";
            else if (string.IsNullOrWhiteSpace(address))
                message = "Ange address";
            else if (string.IsNullOrWhiteSpace(phone))
                message = "Ange telefon";
            else if (!MailHelper.IsValid(email))
                message = "E-post saknas eller ogiltig";
            else if (logicController.IsEmailTaken(username))
                message = "E-postadressen finns redan";
            else
                return true;

            var toast = Toast.MakeText(this, message, ToastLength.Long);
            toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
            toast.Show();
            return false;
        }
    }
}