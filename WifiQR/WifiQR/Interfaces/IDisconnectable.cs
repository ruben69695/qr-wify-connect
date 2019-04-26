using System;
namespace WifiQR.Interfaces
{
    public interface IDisconnectable
    {
        bool TurnOff();
        event EventHandler<string> ConnectionError;
        event EventHandler Disconnected;
    }
}
