using System;
using System.Collections.Generic;

using Xamarin.Forms;
using WifiQR.iOS.Services;
using WifiQR.Interfaces;
using WifiQR.Classes;
using Network;

[assembly: Dependency(typeof(iOSWifiService))]
namespace WifiQR.iOS.Services
{
    public class iOSWifiService : IWifiService
    {
        public iOSWifiService()
        {
            var conn = new NWConnection(new IntPtr(), true);
        }

        public event EventHandler Connected;

        public void ConnectToAccessPoint(string ssid, string password)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccessPoint> GetLastScanAcessPoints()
        {
            return new List<AccessPoint>()
            {
                new AccessPoint("TEST WIFI 1", "ss:65:77:gg"),
                new AccessPoint("TEST WIFI 2", "ss:65:98:gg")
            };
        }

        public bool TurnOff()
        {
            return true;
        }

        public bool TurnOn()
        {
            return true;
        }
    }
}
