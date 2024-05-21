﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MOLPayXDKExample;
using Newtonsoft.Json;

namespace MainActivity
{
    [Activity(Label = "RMSXdkExample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == MOLPayActivity.MOLPayXDK && resultCode == Result.Ok)
            {
                Console.WriteLine("MOLPay result = " + data.GetStringExtra(MOLPayActivity.MOLPayTransactionResult));
                SetContentView(Resource.Layout.layout_molpay);
                TextView tw = (TextView)FindViewById(Resource.Id.resultTV);
                tw.Text = data.GetStringExtra(MOLPayActivity.MOLPayTransactionResult);
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Dictionary<string, object> paymentDetails = new Dictionary<string, object>();
            paymentDetails.Add(MOLPayActivity.mp_amount, "1.01");
            paymentDetails.Add(MOLPayActivity.mp_username, "xxx");
            paymentDetails.Add(MOLPayActivity.mp_password, "xxx");
            paymentDetails.Add(MOLPayActivity.mp_merchant_ID, "xxx");
            paymentDetails.Add(MOLPayActivity.mp_app_name, "xxx");
            paymentDetails.Add(MOLPayActivity.mp_order_ID, "dotnet-maui");
            paymentDetails.Add(MOLPayActivity.mp_verification_key, "xxx");
            paymentDetails.Add(MOLPayActivity.mp_currency, "MYR");
            paymentDetails.Add(MOLPayActivity.mp_country, "MY"); 
            paymentDetails.Add(MOLPayActivity.mp_channel, "multi");
            paymentDetails.Add(MOLPayActivity.mp_bill_description, "description");
            paymentDetails.Add(MOLPayActivity.mp_bill_name, "name");
            paymentDetails.Add(MOLPayActivity.mp_bill_email, "example@email.com");
            paymentDetails.Add(MOLPayActivity.mp_bill_mobile, "+60123456789");
            paymentDetails.Add(MOLPayActivity.mp_channel_editing, false);
            paymentDetails.Add(MOLPayActivity.mp_editing_enabled, false);
            paymentDetails.Add(MOLPayActivity.mp_dev_mode, false);

            Intent intent = new Intent(this, typeof(MOLPayActivity));
            intent.PutExtra(MOLPayActivity.MOLPayPaymentDetails, JsonConvert.SerializeObject(paymentDetails));
            StartActivityForResult(intent, MOLPayActivity.MOLPayXDK);
        }
    }
}