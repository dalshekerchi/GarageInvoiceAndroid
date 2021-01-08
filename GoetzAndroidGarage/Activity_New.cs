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
using System.Data;

namespace GoetzAndroidGarage
{
    [Activity(Label = "Activity_New")]
    public class Activity_New : Activity
    {
        MySqlConnection connection = new MySqlConnection();

        private string VehicleName;
        private string VehicleModel;
        private List<string> VehicleMake;
        private List<string> Toyota;
        private List<string> Honda;
        private List<string> Ford;
        private List<string> Dodge;
        private List<string> Hyundai;
        private List<string> Acura;
        private List<string> Audi;
        private List<string> Chevrolet;
        private List<string> Kia;
        private List<string> Jeep;

        protected override void OnCreate(Bundle savedInstanceState)
        {         
            // Setcontentview allows us to use resources from the NewRecord xml 
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewRecord);

            var CustomerInput = FindViewById<EditText>(Resource.Id.InputCustomer);
            var PhoneNumber = FindViewById<EditText>(Resource.Id.InputPhone);
            var VehicleNumber = FindViewById<EditText>(Resource.Id.InputVehicleYear);
            var VinNumber = FindViewById<EditText>(Resource.Id.InputVIN);
            var LicensePlate = FindViewById<EditText>(Resource.Id.InputLicensePlate);
            var Date = FindViewById<DatePicker>(Resource.Id.InputDate);
            var KM = FindViewById<EditText>(Resource.Id.InputKm);
            var btnSubmit = FindViewById<Button> ( Resource.Id.btnSubmit );
            btnSubmit.Click += BtnSubmit_Click;

            
            // Two spinners are made, one for the Vehicle Make and one for the model.
            // The first spinner handles the Vehicle make and depending on which object from the list is selected,
            // The second spinner changes. Conditions are given depending on the string of the first spinner.
            // Multiple lists consisting of the data of each object in the first spinner.
            // After first spinner and the dynamic second spinner is selected, both object strings are saved into variables to be transferred.


            Spinner spinner1 = FindViewById<Spinner>(Resource.Id.SpinnerVehicleMake);
            Spinner spinner2 = FindViewById<Spinner>(Resource.Id.SpinnerVehicleModel);

            VehicleMake = new List<string> { "Toyota", "Honda", "Ford", "Dodge", "Hyundai", "Acura", "Audi", "Chevrolet", "Kia", "Jeep" };
            List<string> VehicleMakeNames = new List<string>();
            foreach (var item in VehicleMake) VehicleMakeNames.Add(item);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, VehicleMakeNames);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner1.Adapter = adapter;

            Toyota = new List<string> { "Corolla", "RAV4", "Camry" };
            List<string> ToyotaNames = new List<string>();
            foreach (var item in Toyota) ToyotaNames.Add(item);
            var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, ToyotaNames);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner2.Adapter = adapter2;

            VehicleModel = spinner2.SelectedItem.ToString();

            spinner1.ItemSelected += (sender, args) =>
            {

                VehicleName = VehicleMake[args.Position].ToString();

                if (VehicleMake[args.Position] == "Toyota")
                {
                    Toyota = new List<string> { "Corolla", "RAV4", "Camry" };
                    List<string> ToyotaNames = new List<string>();
                    foreach (var item in Toyota) ToyotaNames.Add(item);
                    var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, ToyotaNames);
                    adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    spinner2.Adapter = adapter2;

                    VehicleModel = spinner2.SelectedItem.ToString();
                }
                else if (VehicleMake[args.Position] == "Honda")
                {
                    Honda = new List<string> { "Civic", "Insight", "Accord" };
                    List<string> HondaNames = new List<string>();
                    foreach (var item in Honda) HondaNames.Add(item);
                    var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, HondaNames);
                    adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    adapter2.NotifyDataSetChanged();
                    spinner2.Adapter = adapter2;

                    VehicleModel = spinner2.SelectedItem.ToString();
                }               
                else if (VehicleMake[args.Position] == "Ford")
                {
                    Ford = new List<string> { "Focus", "Explorer", "Escape" };
                    List<string> FordNames = new List<string>();
                    foreach (var item in Ford) FordNames.Add(item);
                    var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, FordNames);
                    adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    adapter2.NotifyDataSetChanged();
                    spinner2.Adapter = adapter2;

                    VehicleModel = spinner2.SelectedItem.ToString();
                }
                else if (VehicleMake[args.Position] == "Dodge")
                {
                    Dodge = new List<string> { "Grand Caravan", "Challenger", "Journey" };
                    List<string> DodgeNames = new List<string>();
                    foreach (var item in Dodge) DodgeNames.Add(item);
                    var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, DodgeNames);
                    adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    adapter2.NotifyDataSetChanged();
                    spinner2.Adapter = adapter2;

                    VehicleModel = spinner2.SelectedItem.ToString();
                }
                else if (VehicleMake[args.Position] == "Hyundai")
                {
                    Hyundai = new List<string> { "Elantra", "Santa Fe", "Sonata" };
                    List<string> HyundaiNames = new List<string>();
                    foreach (var item in Hyundai) HyundaiNames.Add(item);
                    var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, HyundaiNames);
                    adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    adapter2.NotifyDataSetChanged();
                    spinner2.Adapter = adapter2;

                    VehicleModel = spinner2.SelectedItem.ToString();
                }
                else if (VehicleMake[args.Position] == "Acura")
                {
                    Acura = new List<string> { "RDX", "MDX", "RLX" };
                    List<string> AcuraNames = new List<string>();
                    foreach (var item in Acura) AcuraNames.Add(item);
                    var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, AcuraNames);
                    adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    adapter2.NotifyDataSetChanged();
                    spinner2.Adapter = adapter2;

                    VehicleModel = spinner2.SelectedItem.ToString();
                }
                else if (VehicleMake[args.Position] == "Audi")
                {
                    Audi = new List<string> { "A4", "A3", "A5" };
                    List<string> AudiNames = new List<string>();
                    foreach (var item in Audi) AudiNames.Add(item);
                    var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, AudiNames);
                    adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    adapter2.NotifyDataSetChanged();
                    spinner2.Adapter = adapter2;

                    VehicleModel = spinner2.SelectedItem.ToString();
                }
                else if (VehicleMake[args.Position] == "Chevrolet")
                {
                    Chevrolet = new List<string> { "Camaro", "Spark", "Malibu" };
                    List<string> ChevroletNames = new List<string>();
                    foreach (var item in Chevrolet) ChevroletNames.Add(item);
                    var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, ChevroletNames);
                    adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    adapter2.NotifyDataSetChanged();
                    spinner2.Adapter = adapter2;

                    VehicleModel = spinner2.SelectedItem.ToString();
                }
                else if (VehicleMake[args.Position] == "Kia")
                {
                    Kia = new List<string> { "Rio", "Optima", "Soul" };
                    List<string> KiaNames = new List<string>();
                    foreach (var item in Kia) KiaNames.Add(item);
                    var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, KiaNames);
                    adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    adapter2.NotifyDataSetChanged();
                    spinner2.Adapter = adapter2;

                    VehicleModel = spinner2.SelectedItem.ToString();
                }
                else if (VehicleMake[args.Position] == "Jeep")
                {
                    Jeep = new List<string> { "Wrangler", "Cherokee", "Compase" };
                    List<string> JeepNames = new List<string>();
                    foreach (var item in Jeep) JeepNames.Add(item);
                    var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, JeepNames);
                    adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    adapter2.NotifyDataSetChanged();
                    spinner2.Adapter = adapter2;

                    VehicleModel = spinner2.SelectedItem.ToString();
                }
            };
        }

        
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            var CustomerInput = FindViewById<EditText>(Resource.Id.InputCustomer);
            var PhoneNumber = FindViewById<EditText>(Resource.Id.InputPhone);
            var VehicleNumber = FindViewById<EditText>(Resource.Id.InputVehicleYear);
            var VinNumber = FindViewById<EditText>(Resource.Id.InputVIN);
            var LicensePlate = FindViewById<EditText>(Resource.Id.InputLicensePlate);
            var Date = FindViewById<DatePicker>(Resource.Id.InputDate);
            var KM = FindViewById<EditText>(Resource.Id.InputKm);
            if (CustomerInput.Text.ToString().Length > 0 && PhoneNumber.Text.ToString().Length > 0 && VehicleNumber.Text.ToString().Length > 0 && VinNumber.Text.ToString().Length > 0 && LicensePlate.Text.ToString().Length > 0 && Date.ToString().Length > 0 && KM.Text.ToString().Length > 0)
            {
                invoiceDisplay(CustomerInput.Text, PhoneNumber.Text, VehicleNumber.Text, VinNumber.Text, LicensePlate.Text, Date.DateTime.ToString(), KM.Text, VehicleName.ToString(), VehicleModel.ToString());
            }
            else
            {
                Toast.MakeText(this, "Please fill in all the information", ToastLength.Long).Show();
            }
        }
        // Invoice display button that has all the inputted variables by the user in the parameter, this method is called when the button is clicked in order to transfer onto the next xml file and transfer all the data
        private void invoiceDisplay(string CustomerInput, string PhoneNum, string VehicleNum, string VinNum, string LicensePlate, string Date, string KM, string VehicleMake, string VehicleModel)
        {
            // Intent is used to transport to the next xml design file
            // each intent put extra is transferring each variable to the other xml file, the string is referred to as the "Key" which is used to specify which variable is chosen
            Intent intent = new Intent(this, typeof(Activity_InvoiceDisplay));
            intent.PutExtra("CustomerInput", CustomerInput);
            intent.PutExtra("PhoneNumber", PhoneNum);
            intent.PutExtra("VehicleNum", VehicleNum);
            intent.PutExtra("VinNum", VinNum);
            intent.PutExtra("LicensePlate", LicensePlate);
            intent.PutExtra("Date", Date);
            intent.PutExtra("KM", KM);
            intent.PutExtra("VehicleMake", VehicleMake);
            intent.PutExtra("VehicleModel", VehicleModel);
            StartActivity(intent);
        }

    }
}