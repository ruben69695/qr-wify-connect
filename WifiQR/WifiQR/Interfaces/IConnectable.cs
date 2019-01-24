using System;
namespace WifiQR.Interfaces
{
    public interface IConnectable
    {
        bool        TurnOn          ();
        event       EventHandler    Connected;
    }
}
