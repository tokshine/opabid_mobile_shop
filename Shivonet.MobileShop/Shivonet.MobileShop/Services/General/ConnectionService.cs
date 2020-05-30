﻿using Shivonet.MobileShop.Core.Contracts.Services.General;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace Shivonet.MobileShop.Core.Services.General
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConnectivity _connectivity;

        //is this checking for internet connectivity
        public ConnectionService()
        {
            _connectivity = CrossConnectivity.Current;
            _connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            ConnectivityChanged?.Invoke(this, new ConnectivityChangedEventArgs() { IsConnected = e.IsConnected });
        }

        public bool IsConnected => _connectivity.IsConnected;

        public event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
