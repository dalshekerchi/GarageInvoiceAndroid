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
using MySql.Data.MySqlClient;

namespace GoetzAndroidGarage
{
    [Activity(Label = "Activity_View")]
    public class Activity_View : Activity
    {
        MySqlConnection connection = new MySqlConnection();


        // Initalizing services list
        List<string> services = new List<string>();
        protected override void OnCreate(Bundle bundle)
        {

            // The connection string contains the location of the onlie database
            connection.ConnectionString = "datasource=192.168.125.30;port=3306;Database=catalogue;Uid=rootX;Pwd=";
            base.OnCreate(bundle);        
            SetContentView(Resource.Layout.ViewRecord);

            // Spinner  is initialized and calls for the nameList method that reads from the database and loads in the list of names
            // the Adapter is set to the the selected item from the spinner
            // The selected item calls for the invoice display method which then is transferred onto the next form


            Spinner spinner = FindViewById<Spinner>(Resource.Id.Names);

            nameList(spinner);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, services);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            spinner.ItemSelected += Spinner_ItemSelected;

        }
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.Names);
            FindViewById<Button>(Resource.Id.btnConfirm).Click += (i, o) =>
            invoiceDisplay(services[e.Position].ToString());
        }

        private void nameList(Spinner spinner)
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "Select CustomerName From tblhistory";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    services.Add(reader["CustomerName"].ToString());
                }
                reader.Close();
            }
            catch (Exception a)
            {
                
            }
            finally
            {
                connection.Close();
            }
        }

        private void invoiceDisplay(string Name)
        {
            Intent intent2 = new Intent(this, typeof(Activity_ViewInvoice));
            intent2.PutExtra("Customer", Name);
            StartActivity(intent2);
        }
    }
}