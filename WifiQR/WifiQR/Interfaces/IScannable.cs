using System;
namespace WifiQR.Interfaces
{
    public interface IScannable
    {
        void                                Scan                        ();
        event       EventHandler            ScanStarted;
        event       EventHandler            ScanFinished;
    }
}
