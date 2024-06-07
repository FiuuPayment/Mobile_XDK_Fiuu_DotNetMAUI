using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FiuuXDKExample;
using Newtonsoft.Json;

namespace MainActivity
{
    [Activity(Label = "Fiuu_XdkExample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == FiuuActivity.FiuuXDK && resultCode == Result.Ok)
            {
                Console.WriteLine("Fiuu result = " + data.GetStringExtra(FiuuActivity.FiuuTransactionResult));
                SetContentView(Resource.Layout.layout_fiuu);
                TextView tw = (TextView)FindViewById(Resource.Id.resultTV);
                tw.Text = data.GetStringExtra(FiuuActivity.FiuuTransactionResult);
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Dictionary<string, object> paymentDetails = new Dictionary<string, object>();
            paymentDetails.Add(FiuuActivity.mp_amount, "1.01");
            paymentDetails.Add(FiuuActivity.mp_username, "xxx");
            paymentDetails.Add(FiuuActivity.mp_password, "xxx");
            paymentDetails.Add(FiuuActivity.mp_merchant_ID, "xxx");
            paymentDetails.Add(FiuuActivity.mp_app_name, "xxx");
            paymentDetails.Add(FiuuActivity.mp_order_ID, "dotnet-maui");
            paymentDetails.Add(FiuuActivity.mp_verification_key, "xxx");
            paymentDetails.Add(FiuuActivity.mp_currency, "MYR");
            paymentDetails.Add(FiuuActivity.mp_country, "MY"); 
            paymentDetails.Add(FiuuActivity.mp_channel, "multi");
            paymentDetails.Add(FiuuActivity.mp_bill_description, "description");
            paymentDetails.Add(FiuuActivity.mp_bill_name, "name");
            paymentDetails.Add(FiuuActivity.mp_bill_email, "example@email.com");
            paymentDetails.Add(FiuuActivity.mp_bill_mobile, "+60123456789");
            paymentDetails.Add(FiuuActivity.mp_channel_editing, false);
            paymentDetails.Add(FiuuActivity.mp_editing_enabled, false);

            Intent intent = new Intent(this, typeof(FiuuActivity));
            intent.PutExtra(FiuuActivity.FiuuPaymentDetails, JsonConvert.SerializeObject(paymentDetails));
            StartActivityForResult(intent, FiuuActivity.FiuuXDK);
        }
    }
}