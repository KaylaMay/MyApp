using System;
using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using MyApp.Models;

namespace MyApp
{
    [Activity(Label = "registerActivity", MainLauncher = false)]
    public class registerActivity : Activity
    {
        Button btnRegister;
        static string url = "http://10.0.2.2:8080/api/Register";
        EditText a, b, c, d;
        HttpClient client;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register);

            // Create your application here
            a = FindViewById<EditText>(Resource.Id.txtname);
            b = FindViewById<EditText>(Resource.Id.txtsurname);
            c = FindViewById<EditText>(Resource.Id.txtemail);
            d = FindViewById<EditText>(Resource.Id.txtpassword);

            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            btnRegister.Click += btnRegister_Clicked;
        }
        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                client = new HttpClient();
                var myClient = new customer
                {
                    name = a.Text,
                    surname = b.Text,
                    email = c.Text,
                    password = d.Text
                };

                a.Text = "";
                b.Text = "";
                c.Text = "";
                d.Text = "";

                var uri = new Uri(string.Format(url));
                var json = JsonConvert.SerializeObject(myClient);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);
                //Toast.MakeText(this, "Thank you for registering with UberEats", ToastLength.Long).Show();

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    customer custs = JsonConvert.DeserializeObject<customer>(data);
                    Toast.MakeText(this, "Thank you for registering with UberEats", ToastLength.Long).Show();
                    Intent ip = new Intent(this, typeof(MainActivity));
                    StartActivity(ip);
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }


        }
    }
}
