using System;
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
            WifisList.ItemsSource = AccessPoints;

            WifiService.Connected += WifiService_Connected;
            WifiService.ConnectionError += WifiService_ConnectionError;
            WifiService.RefreshDone += WifiService_RefreshDone;
        }

        private ObservableCollection<AccessPointView> GetAccessPointsView()
        {
            var items = new ObservableCollection<AccessPointView>();

            foreach (AccessPoint item in WifiService.GetLastScanAccessPoints())
            {
                items.Add(new AccessPointView(item.SSID, "baseline_network_wifi_black_24", item.BSSID));
            }

            return items;
        }

        private async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (WifiService.IsRefreshing)
                return;
            
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();

            if (result != null)
                WifiService.ConnectToAccessPoint((e.Item as AccessPoint).SSID, result.Text);
        }

        private void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            WifisList.SelectedItem = null;
        }

        private void WifiService_Connected(object sender, System.EventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                lblStatus.Text = "Connected";
                lblStatus.FadeTo(1.0d, 1500, Easing.SpringIn);
            });
        }
        
        private void WifiService_ConnectionError(object sender, string e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                lblStatus.Text = $"Error connecting";
                lblStatus.FadeTo(1.0d, 500, Easing.SpringIn);
            });
        }

        private void WifiService_RefreshDone(object sender, EventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                ScanButton.IsEnabled = true;
            
                LoadingIndicator.IsVisible = false;
                LoadingIndicator.IsRunning = false;

                lblStatus.Text = $"Scan done";
                lblStatus.FadeTo(0.0d, 2500, Easing.CubicOut);
            
                AccessPoints = GetAccessPointsView();
                WifisList.ItemsSource = AccessPoints;
                WifisList.IsVisible = true;
            });
        }

        private void BtnRefresh_OnClicked(object sender, EventArgs e)
        {
            WifiService.Refresh();

            ScanButton.IsEnabled = false;
            
            lblStatus.Text = $"Scanning...";
            lblStatus.FadeTo(1.0d, 1500, Easing.SpringIn);
            
            WifisList.IsVisible = false;
            
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;
        }
    }
}
