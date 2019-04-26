using System;

namespace WifiQR.Interfaces
{
    public interface IRefreshable
    {
        event EventHandler RefreshDone;
        bool IsRefreshing { get; }
        void Refresh();
    }
}