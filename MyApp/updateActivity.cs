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
    [Activity(Label = "updateActivity", MainLauncher = false)]
    public class updateActivity : Activity
    {
        Button btnUpdate;
        EditText a, b, c, d, p;
        RadioGroup radgroup;
        HttpClient client;
        string email;
        string password;
        DataAccess dta = new DataAccess();
        customer cu = new customer();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.update);

            // Create your application here
            email = Intent.GetStringExtra("email");
            password = Intent.GetStringExtra("password");

            radgroup = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            a = FindViewById<EditText>(Resource.Id.txtCust_id);
            b = FindViewById<EditText>(Resource.Id.txtname);
            c = FindViewById<EditText>(Resource.Id.txtsurname);
            d = FindViewById<EditText>(Resource.Id.txtaddress);
            p = FindViewById<EditText>(Resource.Id.txtnumber);

            a.Text = Convert.ToString(cu.cust_id);
            b.Text = cu.name;

            Toast.MakeText(this, "Update Activity cust_id " + cu.cust_id, ToastLength.Short).Show();
            btnUpdate = FindViewById<Button>(Resource.Id.btnUpdates);
            btnUpdate.Click += BtnUpdate_Clicked;
        }
        private void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                RadioButton checkedgender = FindViewById<RadioButton>(radgroup.CheckedRadioButtonId);

                if (checkedgender.Text == "Female")
                {
                    cu.Gender = "Female";
                }
                else if (checkedgender.Text == "Male")
                {
                    cu.Gender = "Male";
                }

                client = new HttpClient();

                cu.surname = c.Text;
                cu.Address = d.Text;
                cu.phone = p.Text;

                var id = Convert.ToInt32(cu.cust_id);
                dta.UpdateUser(cu, cu.cust_id);

                Toast.MakeText(this, "Update Complete, Thank you " + cu.name, ToastLength.Long).Show();

                a.Text = "";
                b.Text = "";
                c.Text = "";
                d.Text = "";
                p.Text = "";

                Intent intent = new Intent(this, typeof(registerActivity));

                intent.PutExtra("email", email);
                intent.PutExtra("password", password);
                StartActivity(intent);

                Toast.MakeText(this, " Activity cust_id " + cu.cust_id, ToastLength.Short).Show();

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }

        }
    }
}
