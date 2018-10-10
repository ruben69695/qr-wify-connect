namespace WifiQR.Classes
{
    public class AccessPointView : AccessPoint
    {
        public string Icon { get; set; }
        public AccessPointView(string ssid, string icon) : base(ssid) 
            { Icon = icon; }
    }
}
