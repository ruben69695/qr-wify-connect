using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Android.Runtime;
using Android.Net.Wifi;
using Android.Content;

using WifiQR.Interfaces;
using WifiQR.Classes;
using WifiQR.Droid.Services;
using System;
using System.Timers;

[assembly: Dependency(typeof(AndroidWifiService))]
namespace WifiQR.Droid.Services
{
    public class AndroidWifiService : IWifiService
    {
        private readonly WifiManager _manager;
        private WifiConfiguration _current;
        private int _currentNetId;
        private Timer _wifiCheckingTimer;
        private int _totalMiliseconds;

        public event EventHandler Connected;

        public AndroidWifiService()
        {
            _manager = Android.App.Application.Context.GetSystemService(Context.WifiService).JavaCast<WifiManager>();
            _wifiCheckingTimer = new Timer(1000);
            _wifiCheckingTimer.Elapsed += _wifiCheckingTimer_Elapsed;
        }

        public void ConnectToAccessPoint(string ssid, string password)
        {
            int netId = -1;

            var wifi = new WifiConfiguration
            {
                Ssid =  $"\"{ssid}\"",
                PreSharedKey = $"\"{password}\"",
            };

            netId = _manager.ConfiguredNetworks.FirstOrDefault(x => x.Ssid == ssid) != null 
                            ? _manager.UpdateNetwork(wifi) 
                            : _manager.AddNetwork(wifi);

            _currentNetId = netId;
            _current = wifi;

            //int netId = _manager.AddNetwork(wifi);
            _manager.Disconnect();
            _manager.EnableNetwork(netId, true);
            _manager.Reconnect();

            startWifiCheckingTimer();
        }

        public bool TurnOff()
        {
            return _manager.SetWifiEnabled(false);
        }

        public bool TurnOn()
        {
            return _manager.SetWifiEnabled(true);
        }

        public void Dispose()
        {
            _manager.Dispose();
        }

        public IEnumerable<AccessPoint> GetLastScanAcessPoints()
        {
            var accessPoints = new List<AccessPoint>();
            foreach (var item in _manager.ScanResults)
            {
                accessPoints.Add(new AccessPoint(item.Ssid, item.Bssid));
            }
            return accessPoints;
        }

        private void _wifiCheckingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_totalMiliseconds == 10000)
            {
                stopWifiCheckingTimer();
            }
            if (!_manager.IsWifiEnabled)
            {
                stopWifiCheckingTimer();
                return;
            }
            if (_manager.ConnectionInfo == null)
                return;

            if(_manager.ConnectionInfo.NetworkId == _currentNetId && _manager.ConnectionInfo.SSID == _current.Ssid)
            {
                stopWifiCheckingTimer();
                Connected?.Invoke(this, EventArgs.Empty);
            }

            _totalMiliseconds += 1000;
        }

        private void stopWifiCheckingTimer()
        {
            _wifiCheckingTimer.Stop();
        }

        private void startWifiCheckingTimer()
        {
            _wifiCheckingTimer.Start();
        }

    }
}
