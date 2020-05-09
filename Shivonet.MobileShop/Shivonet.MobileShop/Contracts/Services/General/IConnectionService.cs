using Plugin.Connectivity.Abstractions;

namespace Shivonet.MobileShop.Core.Contracts.Services.General
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
