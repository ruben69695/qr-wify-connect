using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WifiQR.Interfaces;
using WifiQR.Classes;
using WifiQR.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WifiQR
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //WifiService.ConnectToAccessPoint("MOVISTAR_ABEE", "949FE08721A5C7FCC341");
            MainPage = new NavigationPage(new AccessPointsPage());
            MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#6200EE"));
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
