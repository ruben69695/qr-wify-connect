using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Net.Wifi;
using Android;
using ZXing.Mobile;

namespace WifiQR.Droid
{
    [Activity(Label = "WifiQR", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            MobileBarcodeScanner.Initialize(Application);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            AskPermissions();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            switch (requestCode)
            {
                case 1:
                    if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                        LoadXamarinApp();
                    else
                        Console.WriteLine("Don't allowed the permissions");
                break;
            }
        }

        /// <summary>
        /// Cargar actividades de xamarin
        /// </summary>
        void LoadXamarinApp()
        {
            LoadApplication(new App());
        }

        /// <summary>
        /// Ask the user to allow the main permissions to work with bluetooth and discover devices with bluetooth on the phone
        /// </summary>
        void AskPermissions()
        {
            int MY_PERMISSIONS_REQUEST_ACCESS_COARSE_LOCATION = 1;
            RequestPermissions(
                new String[] {
                            Manifest.Permission.AccessFineLocation,
                            Manifest.Permission.AccessCoarseLocation }, MY_PERMISSIONS_REQUEST_ACCESS_COARSE_LOCATION);
        }

    }
}