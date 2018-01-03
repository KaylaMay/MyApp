using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyApp.Models;
using Newtonsoft.Json;

namespace MyApp
{
    [Activity(Label = "checkoutActivity")]
    public class checkoutActivity : Activity
    {
        TextView a;
        EditText b, c, d, f;
        HttpClient client;
        Button btnPay;
        string url = "http://10.0.2.2:8080/api/Payments";
        string url1 = "http://10.0.2.2:8080/api/Orders";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.checkout);
            // Create your application here
            a = FindViewById<TextView>(Resource.Id.txtcustomerId);
            b = FindViewById<EditText>(Resource.Id.txtAccno);
            c = FindViewById<EditText>(Resource.Id.txtAccHolder);
            d = FindViewById<EditText>(Resource.Id.txtCvv);
            f = FindViewById<EditText>(Resource.Id.txtDAddress);

            btnPay = FindViewById<Button>(Resource.Id.btnProceed);
            btnPay.Click += BtnPay_Clicked;
        }
        private async void BtnPay_Clicked(object sender, EventArgs e)
        {
            try
            {
                client = new HttpClient();
                //Payment
                var cust = new payment()
                {
                    cust_id = Convert.ToInt32(a.Text),
                    account_no = Convert.ToInt32(b.Text),
                    account_holder = Convert.ToString(c.Text),
                    cvv = Convert.ToInt32(d.Text)

                };
                a.Text = "";
                b.Text = "";
                c.Text = "";
                d.Text = "";

                //Going to the Payment Table
                var uri = new Uri(string.Format(url));
                var json = JsonConvert.SerializeObject(cust);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    payment objpay = JsonConvert.DeserializeObject<payment>(data);
                    Toast.MakeText(this, "Payment successful", ToastLength.Long).Show();
                }
                //Order
                var user = new orders()
                {
                    Delivery_Address = Convert.ToString(f.Text)
                };

                f.Text = "";

                //Going to the Orders Table
                var uri1 = new Uri(string.Format(url1));
                var json1 = JsonConvert.SerializeObject(user);
                var content1 = new StringContent(json1, Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = null;
                response1 = await client.PostAsync(uri1, content1);

                if (response1.IsSuccessStatusCode)
                {
                    var data1 = await response1.Content.ReadAsStringAsync();
                    orders Order = JsonConvert.DeserializeObject<orders>(data1);
                    Toast.MakeText(this, "and your order successfully Placed", ToastLength.Long).Show();
                    Intent ti = new Intent(this, typeof(MainActivity));
                    StartActivity(ti);
                }


            }
            catch (Exception)
            {
                Toast.MakeText(this, "Payment and Order Not Processed", ToastLength.Long).Show();
            }

        }
    }
}
