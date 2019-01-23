using System;
using System.ComponentModel;
using WifiQR.Interfaces;
namespace WifiQR.Classes
{
    public class AccessPoint : INotifyPropertyChanged
    {
        private readonly    string  _ssid           = "";
        private readonly    string  _bssid          = "";
        private             string  _password       = "";
        private             bool    _connected      = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public string SSID
        {
            get => _ssid;
        }

        public string BSSID
        {
            get => _bssid;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public bool Connected
        {
            get => _connected;
            set
            {
                _connected = value; 
                OnPropertyChanged(nameof(Connected));
            }
        }

        public AccessPoint(string ssid, string bssid)
        {
            _ssid = ssid == string.Empty ? "[NO SSID]" : ssid;
            _bssid = bssid == string.Empty ? "[NO BSSID]" : bssid;
        }

        public AccessPoint(string ssid, string password, string bssid) 
            : this(ssid, bssid)
        {
            _password = password;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
