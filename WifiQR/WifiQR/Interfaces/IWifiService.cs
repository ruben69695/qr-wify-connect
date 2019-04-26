using System;
using System.Collections.Generic;
using WifiQR.Classes;

namespace WifiQR.Interfaces
{
    public interface IWifiService : IConnectable, IDisconnectable, IRefreshable, IDisposable
    {
        IEnumerable<AccessPoint>    GetLastScanAccessPoints              ();
        void                        ConnectToAccessPoint                (string ssid, string password);
    }
}
