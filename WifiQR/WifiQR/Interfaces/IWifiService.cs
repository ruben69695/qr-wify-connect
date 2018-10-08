using System;
using System.Collections.Generic;
using WifiQR.Classes;

namespace WifiQR.Interfaces
{
    public interface IWifiService : IConnectable, IDisconnectable, IDisposable
    {
        IEnumerable<AccessPoint>    GetLastScanAcessPoints              ();
        void                        ConnectToAccessPoint                (string ssid, string password);
    }
}
