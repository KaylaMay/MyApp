using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;
using System;   

namespace MyApp
{
    [Activity(Label = "MyApp", MainLauncher = true)]
    public class MainActivity : Activity
    {

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.btnLogIn:
                    var intent = new Intent(this, typeof(loginActivity));
                    StartActivity(intent);
                    return true;
                case Resource.Id.btnRegister:
                    intent = new Intent(this, typeof(registerActivity));
                    StartActivity(intent);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
        }

    }
}

