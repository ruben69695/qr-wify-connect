using System;

using Xamarin.Forms;
using WifiQR.iOS.Services;
using WifiQR.Interfaces;
using System.Collections.Generic;
using WifiQR.Classes;

[assembly: Dependency(typeof(iOSWifiService))]
namespace WifiQR.iOS.Services
{
    public class iOSWifiService : IWifiService
    {
        public iOSWifiService()
        {
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
