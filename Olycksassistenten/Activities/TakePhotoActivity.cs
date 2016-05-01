using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Android.Content;
using Newtonsoft.Json;
using Olycksassistenten.Logic;
using Environment = Android.OS.Environment;
using File = Java.IO.File;
using Uri = Android.Net.Uri;

namespace Olycksassistenten.Activities
{
    [Activity(Label = "TakePhotoActivity")]
    public class TakePhotoActivity : Activity
    {
        private File _pictureFolder;
        private string _currentPicture;
        private List<string> _allPictures;

        public TakePhotoActivity()
        {
            _pictureFolder = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures);
            _allPictures = new List<string>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.TakePhoto);

            var buttonTakePhoto = FindViewById<Button>(Resource.Id.buttonTakePhoto);
            buttonTakePhoto.Click += TakePhoto;

            var buttonAddPhotoOk = FindViewById<Button>(Resource.Id.buttonAddPhotoOk);
            buttonAddPhotoOk.Click += TakePhotoOkClick;
        }

        private void TakePhoto(object sender, EventArgs e)
        {
            _currentPicture = String.Format("olycka_{0}.jpg", DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            //starta kameraapplikationen
            var intent = new Intent(MediaStore.ActionImageCapture);
            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(new File(_pictureFolder, _currentPicture)));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            var file = new File(_pictureFolder, _currentPicture);
            if (file.Exists())
            {
                //visa foto
                var imageView = FindViewById<ImageView>(Resource.Id.imageViewTakePhotoWindow);
                imageView.SetImageURI(Uri.FromFile(file));

                //spara foto
                _allPictures.Add(file.Path);

                //visa meddelande
                var toast = Toast.MakeText(this, string.Format("{0} bild(er) tillagda i anmälan", _allPictures.Count), ToastLength.Long);
                toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
                toast.Show();
            }
        }

        private void TakePhotoOkClick(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                var user1 = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user1"));
                var insuranceCompany1 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany1"));
                var insuranceCompany2 = JsonConvert.DeserializeObject<InsuranceCompany>(Intent.GetStringExtra("insuranceCompany2"));
                var vehicle1 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle1"));
                var vehicle2 = JsonConvert.DeserializeObject<Vehicle>(Intent.GetStringExtra("vehicle2"));
                var pictures = _allPictures.ToArray();

                //starta ny aktivitet och skicka med parametrar
                var activity = new Intent(this, typeof(DescriptionAccidentActivity));
                activity.PutExtra("user1", JsonConvert.SerializeObject(user1));
                activity.PutExtra("insuranceCompany1", JsonConvert.SerializeObject(insuranceCompany1));
                activity.PutExtra("insuranceCompany2", JsonConvert.SerializeObject(insuranceCompany2));
                activity.PutExtra("vehicle1", JsonConvert.SerializeObject(vehicle1));
                activity.PutExtra("vehicle2", JsonConvert.SerializeObject(vehicle2));
                activity.PutExtra("pictures", pictures);
                StartActivity(activity);
            }
        }

        private bool ValidateInput()
        {
            string message;
            if (_allPictures.Count == 0)
                message = "Tag en eller flera foton på olyckan";
            else
                return true;

            var toast = Toast.MakeText(this, message, ToastLength.Long);
            toast.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal, 0, 10);
            toast.Show();
            return false;
        }
    }
}