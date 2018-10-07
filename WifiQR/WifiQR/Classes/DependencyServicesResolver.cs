using System;
using Xamarin.Forms;

using WifiQR.Interfaces;
namespace WifiQR.Classes
{
    public sealed class DependencyServicesResolver
    {
        private static IWifiService _instance = null;

        private DependencyServicesResolver() 
        {
            // Register dependencies
            DependencyService.Register<IWifiService>();
        }

        public static IWifiService WifiService 
        {
            get 
            {
                if(_instance == null)
                {
                    _instance = DependencyService.Get<IWifiService>();
                }

                return _instance;
            }
        }
    }
}
