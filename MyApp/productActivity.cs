
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
    [Activity(Label = "productActivity")]
    public class productActivity : Activity
    {
        static string uri = "http://10.0.2.2:8080/api/Products";
        public static Context contextt;
        private static List<product> prod = new List<product>();
        static ListView listView;
        static string email;
        static string password;
        static int quantity = 0;
        static double interim = 0;

        static Intent intent = new Intent();
        static List<product> pd = new List<product>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.product);
            // Create your application here
            listView = FindViewById<ListView>(Resource.Id.lstProducts);

            GetProd restau = new GetProd();
            restau.Execute();
            intent = new Intent(this, typeof(cartActivity));

            email = Intent.GetStringExtra("email");
            password = Intent.GetStringExtra("password");
        }
        public class GetProd : AsyncTask
        {
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                HttpClient client = new HttpClient();

                Uri url = new Uri(uri);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                var product = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<product>>(product);

                foreach (var g in result)
                {
                    prod.Add(g);
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
                listView.Adapter = new ProImageAdapter(contextt, prod);
            }
        }

        public class ProImageAdapter : BaseAdapter<product>
        {
            private List<product> prope = new List<product>();
            static Context context;

            public ProImageAdapter(Context con, List<product> lstP)
            {
                prope.Clear();
                context = con;
                prope = lstP;
                this.NotifyDataSetChanged();
            }

            public override product this[int position]
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
                View products = convertView;
                if (products == null)
                {
                    products = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.searchProduct, parent, false);
                }
                TextView prod_Name = products.FindViewById<TextView>(Resource.Id.txtProdname);
                TextView prod_Price = products.FindViewById<TextView>(Resource.Id.txtProdPrice);
                ImageView prod_Pic = products.FindViewById<ImageView>(Resource.Id.imgProdpic);
                Button btnCart;


                btnCart = products.FindViewById<Button>(Resource.Id.btnCart);
                btnCart.Click += BtnCart_Clicked;

                void BtnCart_Clicked(object sender, EventArgs e)
                {
                    double tot = 0;
                    int q = 0;
                    double Total = 0;
                    string Name = "";
                    double Price = 0;
                    intent.PutExtra("ProdName", Name);
                    intent.PutExtra("ProdPrice", Price);
                    intent.PutExtra("quantity", q);
                    intent.PutExtra("total", tot);

                    pd = prod;
                    Total = Convert.ToDouble(pd[position].prod_price);
                    interim += Total; //This I declared at the beginning (The interim)as a static variable do to accumulate the total


                    intent.PutExtra("ProdName", pd[position].prod_name);    /*    This two lines its were I'm passing my prodname and price , each time a button is clicked
                                                                                    so the are passed individually, I'm still trying to figure out how i can pass the whole list*/
                    intent.PutExtra("ProdPrice", pd[position].prod_price);

                    intent.PutExtra("total", interim);

                    quantity++; //to capture quantity, it can be improved in a sense that you can have a separate method to accomodate for increasing and decreasing quantity neh
                                // addedToCart(interim, quantity);
                    intent.PutExtra("quantity", quantity);

                }

                if (prope[position].prod_pic != null)
                {
                    prod_Pic.SetImageBitmap(BitmapFactory.DecodeByteArray(prope[position].prod_pic, 0, prope[position].prod_pic.Length));
                }

                prod_Name.Text = prope[position].prod_name;
                prod_Price.Text = "R " + Convert.ToString(prope[position].prod_price);
                prod_Pic.Tag = prope[position].prod_pic;
                return products;
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu2, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {

                case Resource.Id.btnCaaart:

                    //email = Intent.GetStringExtra("email");
                    //password = Intent.GetStringExtra("password");
                    //cu = dta.GetCusts(email, password);

                    //intent.PutExtra("email", email);
                    //intent.PutExtra("password", password);
                    StartActivity(intent);
                    return true;


                default:
                    return false;
            }
        }
    }
}
