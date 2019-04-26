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
using System.Threading.Tasks;
using System.Timers;
using Thread = System.Threading.Thread;

[assembly: Dependency(typeof(AndroidWifiService))]
namespace WifiQR.Droid.Services
{    
    public class AndroidWifiService : IWifiService
    {
        private readonly WifiManager _manager;
        private readonly Timer _wifiCheckingTimer;
        
        private WifiConfiguration _current;
        private int _currentNetId;
        private int _totalMilliseconds;

        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event EventHandler<string> ConnectionError;
        public event EventHandler RefreshDone;

        public bool IsRefreshing { get; private set; }


        public AndroidWifiService()
        {
            _manager = Android.App.Application.Context.GetSystemService(Context.WifiService).JavaCast<WifiManager>();
            _wifiCheckingTimer = new Timer(1000);
            _wifiCheckingTimer.Elapsed += WifiCheckingTimer_Elapsed;
        }

        #region Public API

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

            _manager.Disconnect();
            _manager.EnableNetwork(netId, true);
            _manager.Reconnect();

            StartWifiCheckingTimer();
        }

        public bool TurnOff()
        {
            Disconnected?.Invoke(this, EventArgs.Empty);
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

        public IEnumerable<AccessPoint> GetLastScanAccessPoints()
        {
            return _manager.ScanResults.Select(item => new AccessPoint(item.Ssid, item.Bssid)).ToList();
        }
        
        public async void Refresh()
        {
            if (IsRefreshing)
                return;

            await Task.Run(() =>
            {
                IsRefreshing = true;

                // There's no other form right now to scan for new wifi access points
                _manager.StartScan();
                Thread.Sleep(7000);

                IsRefreshing = false;
            });

            RefreshDone?.Invoke(this, EventArgs.Empty);
        }
        
        #endregion

        private void WifiCheckingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_totalMilliseconds == 10000)
            {
                StopWifiCheckingTimer();
                ConnectionError?.Invoke(this, "Can't connect, incorrect password");
            }
            if (!_manager.IsWifiEnabled)
            {
                StopWifiCheckingTimer();
                return;
            }
            if (_manager.ConnectionInfo == null)
                return;

            if(_manager.ConnectionInfo.NetworkId == _currentNetId && _manager.ConnectionInfo.SSID == _current.Ssid)
            {
                StopWifiCheckingTimer();
                Connected?.Invoke(this, EventArgs.Empty);
            }

            _totalMilliseconds += 1000;
        }

        private void StopWifiCheckingTimer()
        {
            _wifiCheckingTimer.Stop();
        }

        private void StartWifiCheckingTimer()
        {
            _wifiCheckingTimer.Start();
        }
    }
}
