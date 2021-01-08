using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using MySql.Data.MySqlClient;
using System.Data;

namespace GoetzAndroidGarage
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        MySqlConnection connection = new MySqlConnection();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            connection.ConnectionString = "datasource=192.168.125.30;port=3306;Database=catalogue;Uid=rootX;Pwd=";
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource

            // Main Code

            SetContentView(Resource.Layout.activity_main);

            // Buttons are found and are enabled to click, each method is given a different Intent to transfer onto a seperate xml file
            // the view page method allows the ViewRecord button to transfer the user onto the view page
            FindViewById<Button>(Resource.Id.btnViewRecord).Click += (e, o) =>
            ViewPage();

            // the newpage method allows the NewRecord button to transfer the user onto the NewRecord page
            FindViewById<Button>(Resource.Id.btnNewRecord).Click += (e, o) =>
            NewPage();
        }

        private void ViewPage()
        {
            Intent intent = new Intent(this, typeof(Activity_View));
            StartActivity(intent);
        }

        private void NewPage()
        {
            Intent intent = new Intent(this, typeof(Activity_New));
            StartActivity(intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}