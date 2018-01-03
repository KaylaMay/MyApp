using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyApp.Models;
using Newtonsoft.Json;

namespace MyApp
{
    [Activity(Label = "restaurantActivity")]
    public class restaurantActivity : Activity
    {
        static string uri = "http://10.0.2.2:8080/api/Restaurants";
        public static Context contextt;
        private static List<restaurant> rest = new List<restaurant>();
        static ListView listView;
        static Intent intent = new Intent();
        static string email;
        static string password;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu2, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.btnUpdate:
                    item.SetVisible(false);
                    intent = new Intent(this, typeof(updateActivity));

                    StartActivity(intent);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.restaurant);
            // Create your application here
            listView = FindViewById<ListView>(Resource.Id.lstRests);

            intent = new Intent(this, typeof(productActivity));
            email = Intent.GetStringExtra("email");
            password = Intent.GetStringExtra("password");

            StartActivity(intent);

            intent.PutExtra("email", email);
            intent.PutExtra("password", password);

            GettRestu restau = new GettRestu();
            restau.Execute();
        }
        public class GettRestu : AsyncTask
        {
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                HttpClient client = new HttpClient();

                Uri url = new Uri(uri);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                var restaurant = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<restaurant>>(restaurant);

                foreach (var g in result)
                {
                    rest.Add(g);
                }
                return true;
            }
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
            }
            protected override void OnPostExecute(Java.Lang.Object result)
            {
                base.OnPostExecute(result);
                listView.Adapter = new ProImageAdapter(contextt, rest);
            }
        }

        public class ProImageAdapter : BaseAdapter<restaurant>
        {
            private List<restaurant> prope = new List<restaurant>();
            static Context context;

            public ProImageAdapter(Context con, List<restaurant> lstP)
            {
                prope.Clear();
                context = con;
                prope = lstP;
                this.NotifyDataSetChanged();
            }

            public override restaurant this[int position]
            {
                get
                {
                    return prope[position];
                }
            }

            public override int Count
            {
                get
                {
                    return prope.Count;
                }
            }
            public Context Mcontext
            {
                get;
                private set;
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public Bitmap getBitmap(byte[] getByte)
            {
                if (getByte.Length != 0)
                {
                    return BitmapFactory.DecodeByteArray(getByte, 0, getByte.Length);
                }
                else
                {
                    return null;
                }
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View restuarants = convertView;
                if (restuarants == null)
                {
                    restuarants = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.search, parent, false);
                }
                TextView RestName = restuarants.FindViewById<TextView>(Resource.Id.txtrestName);
                TextView RestLoc = restuarants.FindViewById<TextView>(Resource.Id.txtrestLocation);
                TextView RestCell = restuarants.FindViewById<TextView>(Resource.Id.txtPhone);
                ImageView RestPic = restuarants.FindViewById<ImageView>(Resource.Id.imgRest);

                if (prope[position].rest_pic != null)
                {
                    RestPic.SetImageBitmap(BitmapFactory.DecodeByteArray(prope[position].rest_pic, 0, prope[position].rest_pic.Length));
                }

                RestName.Text = prope[position].rest_name;
                RestLoc.Text = prope[position].rest_location;
                RestCell.Text = prope[position].phone;
                RestPic.Tag = prope[position].rest_pic;



                return restuarants;
            }
        }

    }
}
