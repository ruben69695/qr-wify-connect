using System.Collections.Generic;

using Android.App;
using Android.Runtime;
using Android.Net.Wifi;
using Android.Content;

using WifiQR.Interfaces;
using WifiQR.Classes;

namespace WifiQR.Droid.Services
{
    public class AndroidWifiService : IWifiService
    {
        readonly WifiManager _manager;

        public AndroidWifiService()
        {
            _manager = Application.Context.GetSystemService(Context.WifiService).JavaCast<WifiManager>();
        }

        public void ConnectToAccessPoint(string ssid, string password)
        {
            var wifi = new WifiConfiguration
            {
                Ssid = ssid,
                PreSharedKey = password,
            };

            if (!_manager.ConfiguredNetworks.Contains(wifi))
                _manager.UpdateNetwork(wifi);
            else
                _manager.AddNetwork(wifi);

            _manager.EnableNetwork(wifi.NetworkId, true);
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

        public IEnumerable<AccessPoint> GetAcessPoints()
        {
            var accessPoints = new List<AccessPoint>();
            foreach (var item in _manager.ScanResults)
            {
                accessPoints.Add(new AccessPoint(item.Ssid));
            }
            return accessPoints;
        }
    }
}
