namespace WifiQR.Classes
{
    public class AccessPointView : AccessPoint
    {
        public string Icon { get; set; }

        public AccessPointView(string ssid, string icon, string bssid) 
            : base(ssid, bssid) 
        { 
            Icon = icon; 
        }
    }
}
