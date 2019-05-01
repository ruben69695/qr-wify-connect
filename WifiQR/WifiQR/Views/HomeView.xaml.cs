using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WifiQR.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
        }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            var elementName = ((Frame) sender).ClassId;

            switch (elementName)
            {
                case "ScanItem":
                    await Navigation.PushAsync(new AccessPointsPage(), true);
                    break;
                case "AddNetworkItem":
                    break;
                case "ShareNetworkItem":
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementName), elementName, null);
            }
        }
    }
}
