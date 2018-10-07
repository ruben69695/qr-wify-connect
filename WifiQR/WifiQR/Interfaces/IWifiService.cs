using System;
using System.Collections.Generic;
using WifiQR.Classes;

namespace WifiQR.Interfaces
{
    public interface IWifiService : IConnectable, IDisconnectable, IDisposable
    {
        IEnumerable<AccessPoint>    GetAcessPoints             ();
        void                        ConnectToAccessPoint    (string ssid, string password);
    }
}
