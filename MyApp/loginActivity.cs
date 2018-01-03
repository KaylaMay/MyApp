
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

namespace MyApp
{
    [Activity(Label = "loginActivity")]
    public class loginActivity : Activity
    {
        Button btnLogIn;
        static string url = "http://10.0.2.2:8080/api/CustomersLogin";
        EditText c, d;
        HttpClient client;
        //bool isLoggedin = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            c = FindViewById<EditText>(Resource.Id.txtemail);
            d = FindViewById<EditText>(Resource.Id.txtpassword);

            btnLogIn = FindViewById<Button>(Resource.Id.btnLogIn);
            btnLogIn.Click += BtnLogIn_Clicked;
        }
        private void BtnLogIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                client = new HttpClient();

                DataAccess dta = new DataAccess();

                customer cust = dta.GetCusts(c.Text, d.Text);

                if (String.IsNullOrEmpty(c.Text) && String.IsNullOrEmpty(d.Text))
                {
                    Toast.MakeText(this, "Please enter informationon all feilds.", ToastLength.Short).Show();
                }
                else if(c.Text == cust.email && d.Text == cust.password)
                    {
                        
                        Toast.MakeText(this, "Successfully logged in " + cust.name, ToastLength.Short).Show();
                        //isLoggedin = true;
                    Intent ti = new Intent(this, typeof(restaurantActivity));
                        ti.PutExtra("email", cust.email);
                        ti.PutExtra("password", cust.password);
                            
                        StartActivity(ti);


                    }
                    else
                    {
                        Toast.MakeText(this, "User does no exist.", ToastLength.Short).Show();
                        Intent t = new Intent(this, typeof(restaurantActivity));
                        StartActivity(t);
                    }
     
            }

            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }
  
    }
}
