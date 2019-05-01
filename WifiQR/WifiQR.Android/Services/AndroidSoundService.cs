using System;
using WifiQR.Droid.Services;
using WifiQR.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidSoundService))]
namespace WifiQR.Droid.Services
{
    public class AndroidSoundService : ISoundService
    {
        private Android.Views.View _root;

        public AndroidSoundService()
        {
        }

        public void KeyboardSoundClick()
        {
            if (_root == null)
                _root = MainActivity.RootView;

            _root.PlaySoundEffect(Android.Views.SoundEffects.Click);
        }
    }
}
