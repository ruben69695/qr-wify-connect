using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WifiQR.Interfaces;
using WifiQR.Classes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WifiQR
{
    public partial class App : Application
    {
        public IWifiService WifiService { get => DependencyServicesResolver.WifiService; }

        public App()
        {
            InitializeComponent();
            WifiService.ConnectToAccessPoint("MOVISTAR_ABEE", "949FE08721A5C7FCC341");
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
