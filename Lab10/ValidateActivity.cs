using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lab10
{
    [Activity(Label = "@string/ApplicationName")]
    public class ValidateActivity : Activity
    {
        private TextView tvResult;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Validate);

            var etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            var etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            var btnValidate = FindViewById<Button>(Resource.Id.btnValidate);
            tvResult = FindViewById<TextView>(Resource.Id.tvResult);

            btnValidate.Click += async (s, e) =>
            {
                string device = Android.Provider.Settings.Secure.GetString(
                    ContentResolver, Android.Provider.Settings.Secure.AndroidId);

                string email = etEmail.Text;
                string password = etPassword.Text;

                await ValidarActividad(device, email, password);
            };
        }

        private async Task ValidarActividad(string device, string email, string password)
        {
            var service = new SALLab10.ServiceClient();
            var response = await service.ValidateAsync(email, password, device);

            string txtResponse = $"{response.Status}\n{response.Fullname}\n{response.Token}";

            tvResult.Text = txtResponse;
        }
    }
}