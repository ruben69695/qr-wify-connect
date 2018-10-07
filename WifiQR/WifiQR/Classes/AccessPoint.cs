using System;

using WifiQR.Interfaces;
namespace WifiQR.Classes
{
    public class AccessPoint
    {
        private readonly    string  _ssid           = "";
        private             string  _password       = "";
        private             bool    _connected      = false;

        public AccessPoint(string ssid)
        {
            _ssid   = ssid;
        }

        public AccessPoint(string ssid, string password) : this(ssid)
        {
            _password = password;
        }

        public string   SSID
        { 
            get     => _ssid; 
        }
        public string   Password  
        { 
            get     => _password; 
            set     => _password = value; 
        }
        public bool     Connected 
        {
            get     => _connected;
        }
    }
}
