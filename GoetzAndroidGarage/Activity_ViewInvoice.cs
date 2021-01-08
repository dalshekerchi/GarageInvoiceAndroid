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
	[Activity(Label = "Activity_ViewInvoice")]
	public class Activity_ViewInvoice : Activity
	{
        MySqlConnection connection = new MySqlConnection();
        protected override void OnCreate(Bundle savedInstanceState)
		{
            connection.ConnectionString = "datasource=192.168.125.30;port=3306;Database=catalogue;Uid=rootX;Pwd=";
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewInvoice);

           // Intent is used to transport to the next xml design file
            // each intent put extra is transferring each variable to the other xml file, the string is referred to as the "Key" which is used to specify which variable is chosen
            Intent intent2 = Intent;
			string Name = Intent.GetStringExtra("Customer");

            try
            {
               // This section reads from the data base depending on the name of the customer and displays all according information 
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "Select * From tblHistory WHERE CustomerName='" + Name + "'";// this retrevies the most recent history from the database so that it could recommend the oil type of what a person with he same model and make choose
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    FindViewById<TextView>(Resource.Id.txtNameResultt).Text = reader["CustomerName"].ToString();
                    FindViewById<TextView>(Resource.Id.txtVehicleMakeResultt).Text = reader["Brand"].ToString();
                    FindViewById<TextView>(Resource.Id.txtVehicleModelResulttttt).Text = reader["Model"].ToString();
                    FindViewById<TextView>(Resource.Id.txtServeResultt).Text = reader["Service"].ToString();
                    FindViewById<TextView>(Resource.Id.txtVINResultt).Text = reader["VIN"].ToString();
                    FindViewById<TextView>(Resource.Id.txtLicensePlateResultt).Text = reader["LicensePlate"].ToString();
                    FindViewById<TextView>(Resource.Id.txtVehicleYearResultt).Text = reader["Year"].ToString();
                    FindViewById<TextView>(Resource.Id.txtPhoneResultt).Text = reader["PhoneNum"].ToString();
                    FindViewById<TextView>(Resource.Id.txtInvoiceNumberResultt).Text = reader["CustomerID"].ToString();
                    reader.Close();
                    connection.Close();
                }
            }
            catch
            {
                connection.Close();
            }

            var btnBack = FindViewById<Button>(Resource.Id.btnBack);
            btnBack.Click += BtnBack_Click;
        }
        // The back button is used in order to go back to the Main Activity Design view
        private void BtnBack_Click(object sender, EventArgs e)
        {
            Intent open = new Intent(this, typeof(MainActivity));
            StartActivity(open);
        }
    }
}