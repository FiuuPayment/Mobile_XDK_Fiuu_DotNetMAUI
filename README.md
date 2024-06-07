<!--
 # license: Copyright © 2011-2024 Fiuu. All Rights Reserved.
 -->

# Mobile_XDK_Fiuu_DotNetMAUI

Fiuu Mobile Payment for .NET MAUI

<img src="https://user-images.githubusercontent.com/38641542/74424311-a9d64000-4e8c-11ea-8d80-d811cfe66972.jpg">

This is a complete and functional payment gateway .NET MAUI Android payment module that is ready to be implemented into Visual Studio as a FiuuXDK module. An example application project (FiuuXdkExample) is provided for FiuuXDK framework integration reference.

## Recommended configurations

- Microsoft Visual Studio Community 2022 (For Windows)

- Package JSON.NET

- Minimum Android API level: 19 ++

- Minimum Android target version: Android 4.4

## Installation

- In the Solution Explorer of Visual Studio, right click on your MAUI Android project name and go to Add -> Existing Item..., on the window that pops up, select FiuuActivity.cs and click Add.

- Copy and paste **layout_fiuu.axml** into the` Resources\layout\` folder of your MAUI Android project. (Create one if the directory does not exist)

- Copy and paste **menu_fiuu.xml** into the `Resources\menu\` folder of your MAUI Android project. (Create one if the directory does not exist)

- In the Solution Explorer of Visual Studio, click the 'Show All Files' button, after that all the files and folders that are pasted just now will be shown. Right click on each of them and click 'Include In Project'.

- Right click on your Android project and select Properties. Select Android Manifest in the window that opens. Check WRITE_EXTERNAL_STORAGE in the list of permissions.

- Install Json.NET by going to `Tools > NuGet Package Manager > Package Manager Console`, and run the following command in the console. You may refer to this website http://www.newtonsoft.com/json.

```
Install-Package Newtonsoft.Json
```

- Override the OnActivityResult function.

```
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
```

## Sample Result

#### Sample transaction result in JSON string:

```

{"status_code":"11","amount":"1.01","chksum":"34a9ec11a5b79f31a15176ffbcac76cd","pInstruction":0,"msgType":"C6","paydate":1459240430,"order_id":"3q3rux7dj","err_desc":"","channel":"Credit","app_code":"439187","txn_ID":"6936766"}

Parameter and meaning:

"status_code" - "00" for Success, "11" for Failed, "22" for *Pending.
(*Pending status only applicable to cash channels only)
"amount" - The transaction amount
"paydate" - The transaction date
"order_id" - The transaction order id
"channel" - The transaction channel description
"txn_ID" - The transaction id generated by Fiuu
```

*Notes: You may ignore other parameters and values not stated above*


#### Sample error result in JSON string:

```
{"Error":"Communication Error"}

Parameter and meaning:

"Communication Error" - Error starting a payment process due to several possible reasons, please contact Fiuu support should the error persists.
1) Internet not available
2) API credentials (username, password, merchant id, verify key)
3) Fiuu server offline.
```

## Prepare the Payment detail object

```
Dictionary<String, object> paymentDetails = new Dictionary<String, object>();

// Optional, REQUIRED when use online Sandbox environment and account credentials.
paymentDetails.Add(FiuuActivity.mp_dev_mode, false);

// Mandatory String. Values obtained from Fiuu
paymentDetails.Add(FiuuActivity.mp_username, "");
paymentDetails.Add(FiuuActivity.mp_password, "");
paymentDetails.Add(FiuuActivity.mp_merchant_ID, "");
paymentDetails.Add(FiuuActivity.mp_app_name, "");
paymentDetails.Add(FiuuActivity.mp_verification_key, "");

// Mandatory String. Payment values
paymentDetails.Add(FiuuActivity.mp_amount, ""); // Minimum 1.01
paymentDetails.Add(FiuuActivity.mp_order_ID, "");
paymentDetails.Add(FiuuActivity.mp_currency, "");
paymentDetails.Add(FiuuActivity.mp_country, ""); 

// Optional, but required payment values. User input will be required when values not passed.
paymentDetails.Add(FiuuActivity.mp_channel, ""); // Use 'multi' for all available channels option. For individual channel seletion, please refer to https://github.com/RazerMS/rms-mobile-xdk-examples/blob/master/channel_list.tsv. 
paymentDetails.Add(FiuuActivity.mp_bill_description, "");
paymentDetails.Add(FiuuActivity.mp_bill_name, "");
paymentDetails.Add(FiuuActivity.mp_bill_email, "");
paymentDetails.Add(FiuuActivity.mp_bill_mobile, "");

// Optional, allow channel selection. 
paymentDetails.Add(FiuuActivity.mp_channel_editing, false);

// Optional, allow billing information editing.    
paymentDetails.Add(FiuuActivity.mp_editing_enabled, false);

// Optional for Escrow
paymentDetails.Add(FiuuActivity.mp_is_escrow, ""); // Optional for Escrow, put "1" to enable escrow

// Optional, for credit card BIN restrictions and campaigns.
String[] binlock = new String[] { "", "" };
paymentDetails.Add(FiuuActivity.mp_bin_lock, binlock);

// Optional, for mp_bin_lock alert error.
paymentDetails.Add(FiuuActivity.mp_bin_lock_err_msg, "");

// WARNING! FOR TRANSACTION QUERY USE ONLY, DO NOT USE THIS ON PAYMENT PROCESS.
// Optional, provide a valid cash channel transaction id here will display a payment instruction screen. Required if mp_request_type is 'Receipt'.
paymentDetails.Add(FiuuActivity.mp_transaction_id, "");
// Optional, use 'Receipt' for Cash channels, and 'Status' for transaction status query.
paymentDetails.Add(FiuuActivity.mp_request_type, "");

// Optional, use this to customize the UI theme for the payment info screen, the original XDK custom.css file can be obtained at https://github.com/RazerMS/rms-mobile-xdk-examples/blob/master/custom.css.
paymentDetails.Add(FiuuActivity.mp_custom_css_url, "file:///A_asset/custom.css");

// Optional, set the token id to nominate a preferred token as the default selection, set "new" to allow new card only.
paymentDetails.Add(FiuuActivity.mp_preferred_token, "");

// Optional, credit card transaction type, set "AUTH" to authorize the transaction.
paymentDetails.Add(FiuuActivity.mp_tcctype, "");

// Optional, required valid credit card channel, set true to process this transaction through the recurring api, please refer the Fiuu Recurring API pdf 
paymentDetails.Add(FiuuActivity.mp_is_recurring, false);

// Optional, show nominated channels.
String[] allowedChannels = new String[] { "", "" };
paymentDetails.Add(FiuuActivity.mp_allowed_channels, allowedChannels);

// Optional, simulate offline payment, set boolean value to enable. 
paymentDetails.Add(FiuuActivity.mp_sandbox_mode, false);

// Optional, required a valid mp_channel value, this will skip the payment info page and go direct to the payment screen.
paymentDetails.Add(FiuuActivity.mp_express_mode, false);

// Optional, extended email format validation based on W3C standards.
paymentDetails.Add(FiuuActivity.mp_advanced_email_validation_enabled, false);

// Optional, extended phone format validation based on Google i18n standards.
paymentDetails.Add(FiuuActivity.mp_advanced_phone_validation_enabled, false);

// Optional, explicitly force disable user input.
paymentDetails.Add(FiuuActivity.mp_bill_name_edit_disabled, true);
paymentDetails.Add(FiuuActivity.mp_bill_email_edit_disabled, true);
paymentDetails.Add(FiuuActivity.mp_bill_mobile_edit_disabled, true);
paymentDetails.Add(FiuuActivity.mp_bill_description_edit_disabled, true);

// Optional, EN, MS, VI, TH, FIL, MY, KM, ID, ZH.
paymentDetails.Add(FiuuActivity.mp_language, "EN");

// Optional, Cash channel payment request expiration duration in hour.
//paymentDetails.Add(FiuuActivity.mp_cash_waittime, "48");

// Optional, allow non-3ds on some credit card channels.
//paymentDetails.Add(FiuuActivity.mp_non_3DS, false);

// Optional, disable card list option.
paymentDetails.Add(FiuuActivity.mp_card_list_disabled, false);
  
// Optional for channels restriction, this option has less priority than mp_allowed_channels.
String disabledChannels[] = {"credit"};
paymentDetails.Add(FiuuActivity.mp_disabled_channels, disabledChannels);

```

## Start the payment module

```
Intent intent = new Intent(this, typeof(FiuuActivity));
intent.PutExtra(FiuuActivity.FiuuPaymentDetails, JsonConvert.SerializeObject(paymentDetails));
StartActivityForResult(intent, FiuuActivity.FiuuXDK);

```

## Cash channel payment process (How does it work?)

    This is how the cash channels work on XDK:

    1) The user initiate a cash payment, upon completed, the XDK will pause at the “Payment instruction” screen, the results would return a pending status.

    2) The user can then click on “Close” to exit the Fiuu XDK aka the payment screen.

    3) When later in time, the user would arrive at say 7-Eleven to make the payment, the host app then can call the XDK again to display the “Payment Instruction” again, then it has to pass in all the payment details like it will for the standard payment process, only this time, the host app will have to also pass in an extra value in the payment details, it’s the “mp_transaction_id”, the value has to be the same transaction returned in the results from the XDK earlier during the completion of the transaction. If the transaction id provided is accurate, the XDK will instead show the “Payment Instruction” in place of the standard payment screen.

    4) After the user done the paying at the 7-Eleven counter, they can close and exit Fiuu XDK by clicking the “Close” button again.

## XDK built-in checksum validator caveats

    All XDK come with a built-in checksum validator to validate all incoming checksums and return the validation result through the "mp_secured_verified" parameter. However, this mechanism will fail and always return false if merchants are implementing the private secret key (which the latter is highly recommended and prefereable.) If you would choose to implement the private secret key, you may ignore the "mp_secured_verified" and send the checksum back to your server for validation.

## Private Secret Key checksum validation formula

    chksum = MD5(mp_merchant_ID + results.msgType + results.txn_ID + results.amount + results.status_code + merchant_private_secret_key)

## Resources
- GitHub:     https://github.com/FiuuPayment
- Website:    https://fiuu.com/
- Twitter:    https://twitter.com/FiuuPayment
- YouTube:    https://www.youtube.com/c/FiuuPayment
- Facebook:   https://www.facebook.com/FiuuPayment/
- Instagram:  https://www.instagram.com/FiuuPayment/


## Support

Submit issue to this repository or email to our support@fiuu.com

Merchant Technical Support / Customer Care : support@fiuu.com<br>
Sales/Reseller Enquiry : sales@fiuu.com<br>
Marketing Campaign : marketing@fiuu.com<br>
Channel/Partner Enquiry : channel@fiuu.com<br>
Media Contact : media@fiuu.com<br>
R&D and Tech-related Suggestion : technical@fiuu.com<br>
Abuse Reporting : abuse@fiuu.com
