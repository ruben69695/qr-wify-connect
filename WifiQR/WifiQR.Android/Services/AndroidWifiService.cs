using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Android.Runtime;
using Android.Net.Wifi;
using Android.Content;

using WifiQR.Interfaces;
using WifiQR.Classes;
using WifiQR.Droid.Services;

[assembly: Dependency(typeof(AndroidWifiService))]
namespace WifiQR.Droid.Services
{
    public class AndroidWifiService : IWifiService
    {
        readonly WifiManager _manager;

        public AndroidWifiService()
        {
            _manager = Android.App.Application.Context.GetSystemService(Context.WifiService).JavaCast<WifiManager>();
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

            //int netId = _manager.AddNetwork(wifi);
            _manager.Disconnect();
            _manager.EnableNetwork(netId, true);
            _manager.Reconnect();
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
                accessPoints.Add(new AccessPoint(item.Ssid));
            }
            return accessPoints;
        }
    }
}
