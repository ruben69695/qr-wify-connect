using System;
using Xamarin.Forms;

using WifiQR.Interfaces;
namespace WifiQR.Classes
{
    public sealed class DependencyServicesResolver
    {
        private static IWifiService _instance = null;
        private static ISoundService _soundInstance = null;

        private DependencyServicesResolver() 
        {
            // Register dependencies
            DependencyService.Register<IWifiService>();
            DependencyService.Register<ISoundService>();
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

        public static ISoundService SoundService
        {
            get
            {
                if(_soundInstance == null)
                {
                    _soundInstance = DependencyService.Get<ISoundService>();
                }

                return _soundInstance;
            }
        }
    }
}
