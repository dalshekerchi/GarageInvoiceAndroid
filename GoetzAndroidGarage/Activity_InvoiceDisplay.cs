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
    [Activity(Label = "Activity_InvoiceDisplay")]
    public class Activity_InvoiceDisplay : Activity
    {
        
        MySqlConnection connection = new MySqlConnection();
        List<string> servicess = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            connection.ConnectionString = "datasource=192.168.125.30;port=3306;Database=catalogue;Uid=rootX;Pwd=";
            // Everything must be below SetContentView
            SetContentView(Resource.Layout.InvoiceDisplay);
            // Intent that passes all the variables from the previous Activity on to this activity and displaying them  
            Intent intent = Intent;
            string CustomerInput = Intent.GetStringExtra("CustomerInput");
            FindViewById<TextView>(Resource.Id.txtNameResult).Text = CustomerInput;

            string PhoneNumber = intent.GetStringExtra("PhoneNumber");
            FindViewById<TextView>(Resource.Id.txtPhoneResult).Text = PhoneNumber;

            string VehicleNumber = intent.GetStringExtra("VehicleNum");
            FindViewById<TextView>(Resource.Id.txtVehicleYearResult).Text = VehicleNumber;

            string VehicleMake = intent.GetStringExtra("VehicleMake");
            FindViewById<TextView>(Resource.Id.txtVehicleMakeResult).Text = VehicleMake;

            string VehicleModel = intent.GetStringExtra("VehicleModel");
            FindViewById<TextView>(Resource.Id.txtVehicleModelResult).Text = VehicleModel;

            string VinNum = intent.GetStringExtra("VinNum");
            FindViewById<TextView>(Resource.Id.txtVINResult).Text = VinNum;

            string LicensePlate = intent.GetStringExtra("LicensePlate");
            FindViewById<TextView>(Resource.Id.txtLicensePlateResult).Text = LicensePlate;

            string Date = intent.GetStringExtra("Date");
            FindViewById<TextView>(Resource.Id.txtDateofCompletionResult).Text = Date;

            string Km = intent.GetStringExtra("KM");
            FindViewById<TextView>(Resource.Id.txtKMResult).Text = Km;

            // Spinner initialization and calling for the serviceslist method that adds each element into the spinner and the submit button being initialized
            Spinner spinner = FindViewById<Spinner>(Resource.Id.Services);
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "Select Max(CustomerID) From tblHistory";//used the select max we can get the highest incoce num and add one as this info will e saves in the next invoice numbert
            int invoice = 0;
            if (command.Equals(DBNull.Value))//this check if the customer id is null then make inthe invoice equal to 1
            {
                invoice = 1;
            }
            else
            {
                invoice = (int)command.ExecuteScalar() + 1;//if not add one to the last invocie number as it is a new invoice
            }
            FindViewById<TextView>(Resource.Id.txtInvoiceNumberResult).Text = Convert.ToString(invoice);//this will display it in the textbox
            connection.Close();
            servicesList(spinner);//get the service names from database to snipper
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, servicess);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            spinner.ItemSelected += Spinner_ItemSelected;//once something has been selected
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.Services);
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "Select Time From tblservice where ServiceName='" + spinner.SelectedItem + "'";//got the time so that we can use it to display how long this service will take
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            double time = Convert.ToDouble(reader["Time"]) * 60;//data saved in table as hours wanted to change it to minutes because its less confusing
            reader.Close();
            MySqlCommand command3 = new MySqlCommand();
            command3.Connection = connection;
            command3.CommandText = "Select Wage From tblgeneral";//get the wage per person per hour for a person
            MySqlDataReader reader3 = command3.ExecuteReader();
            reader3.Read();
            int price = Convert.ToInt16(reader3["Wage"]);
            double hourRate = ((time / 60) * price )*1.13;//got the hourly rate like this because the wage can cahnge since the admin is able to chaneg the wage, therefore we made it dynamic so that the pice would change base on the wage, therefore we choose not to display the price in the table
            connection.Close();
            reader3.Close();
            FindViewById<TextView>(Resource.Id.txtTotalResult).Text = "$" + Convert.ToString(hourRate);
            var btnSubmit1 = FindViewById<Button>(Resource.Id.btnSubmit);//intialized the variable for the button and runs that code
            btnSubmit1.Click += BtnSubmit1_Click1;
        }

        private void BtnSubmit1_Click1(object sender, EventArgs e)
        {
            Intent intent = Intent;
            string CustomerInput = Intent.GetStringExtra("CustomerInput");
            FindViewById<TextView>(Resource.Id.txtNameResult).Text = CustomerInput;

            string PhoneNumber = intent.GetStringExtra("PhoneNumber");
            FindViewById<TextView>(Resource.Id.txtPhoneResult).Text = PhoneNumber;

            string VehicleNumber = intent.GetStringExtra("VehicleNum");
            FindViewById<TextView>(Resource.Id.txtVehicleYearResult).Text = VehicleNumber;

            string VehicleMake = intent.GetStringExtra("VehicleMake");
            FindViewById<TextView>(Resource.Id.txtVehicleMakeResult).Text = VehicleMake;

            string VehicleModel = intent.GetStringExtra("VehicleModel");
            FindViewById<TextView>(Resource.Id.txtVehicleModelResult).Text = VehicleModel;

            string VinNum = intent.GetStringExtra("VinNum");
            FindViewById<TextView>(Resource.Id.txtVINResult).Text = VinNum;

            string LicensePlate = intent.GetStringExtra("LicensePlate");
            FindViewById<TextView>(Resource.Id.txtLicensePlateResult).Text = LicensePlate;

            string Date = intent.GetStringExtra("Date");
            FindViewById<TextView>(Resource.Id.txtDateofCompletionResult).Text = Date;

            string Km = intent.GetStringExtra("KM");
            FindViewById<TextView>(Resource.Id.txtKMResult).Text = Km;
           

            Spinner spinner = FindViewById<Spinner>(Resource.Id.Services);

            servicesList(spinner);
            string services = spinner.SelectedItem.ToString();
            // The process of opening the connection and inserting all the entered information into the Database
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT into catalogue.tblhistory (Brand,Model,Service,Year,VIN,LicensePlate,CustomerName,PhoneNum) values('" + VehicleMake + "','" + VehicleModel + "','" + services + "'," + VehicleNumber + ",'" + VinNum + "','" + LicensePlate + "','" + CustomerInput + "','" + PhoneNumber + "');";//this inserts all the information needed for table history so that it can saved 
                command.ExecuteNonQuery();
                Toast.MakeText(this, "Data Saved", ToastLength.Long).Show();
            }
            catch (Exception x)
            {
                FindViewById<TextView>(Resource.Id.txtNameResult).Text = x.ToString();
            }
            finally
            {
                connection.Close();
            }
        }
        private void servicesList(Spinner spinner)
        {
            // This method opens the connection and reads all the information about the list of services and adds them to the Spinner
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "Select ServiceName From tblservice";
                MySqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    servicess.Add(reader["ServiceName"].ToString());
                }
                connection.Close();
                reader.Close();
            }
            catch (Exception a)
            {
                connection.Close();
            }
        }
    }
}