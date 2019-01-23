using System.Collections.ObjectModel;
using WifiQR.Classes;
using WifiQR.Interfaces;
using Xamarin.Forms;

namespace WifiQR.Views
{
    public partial class AccessPointsPage : ContentPage
    {
        public IWifiService WifiService { get => DependencyServicesResolver.WifiService; }
        public ObservableCollection<AccessPointView> AccessPoints { get; set; }

        public AccessPointsPage()
        {
            InitializeComponent();
            AccessPoints = GetAccessPointsView();
            list.ItemsSource = AccessPoints;
        }

        public ObservableCollection<AccessPointView> GetAccessPointsView()
        {
            var items = new ObservableCollection<AccessPointView>();

            foreach (AccessPoint item in WifiService.GetLastScanAcessPoints())
            {
                items.Add(new AccessPointView(item.SSID, "baseline_network_wifi_black_24.png"));
            }

            return items;
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();

            if (result != null)
                WifiService.ConnectToAccessPoint((e.Item as AccessPoint).SSID, result.Text);
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            list.SelectedItem = null;
        }
    }
}
