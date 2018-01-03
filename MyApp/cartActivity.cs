
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyApp
{
    [Activity(Label = "cartActivity")]
    public class cartActivity : Activity
    {
        Intent intent = new Intent();
        ListView listItems;
        Button btnBuy;
        List<string> Itemlist = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.cart);
            // Create your application here

            listItems = FindViewById<ListView>(Resource.Id.lstCart);
            Itemlist.Add(Intent.GetStringExtra("ProdName"));
            Itemlist.Add(Convert.ToString(Intent.GetDoubleExtra("ProdPrice", 0)));
            Itemlist.Add(Convert.ToString(Intent.GetIntExtra("quantity", 0)));
            Itemlist.Add(Convert.ToString(Intent.GetDoubleExtra("total", 0)));

            btnBuy = FindViewById<Button>(Resource.Id.btnBuy);
            btnBuy.Click += BtnBuy_Clicked;


            ArrayAdapter<string> adapt = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Itemlist);
            listItems.Adapter = adapt;
        }
        private void BtnBuy_Clicked(object sender, EventArgs e)
        {
            intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}
