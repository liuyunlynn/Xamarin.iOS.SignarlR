using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace Xamarin.iOS.SignalR.Client
{
    public class SignalRHubClient
    {
        private static SignalRHubClient _instance;
        public static SignalRHubClient Instance => _instance ?? (_instance = new SignalRHubClient("http://172.16.40.186:59062/", "ServerHub"));

        private readonly HubConnection _connection;
        private readonly IHubProxy _proxy;
        public event EventHandler<string> OnReceiveEvent;

        public SignalRHubClient(string serverUrl, string hubName)
        {
            _connection = new HubConnection(serverUrl);
            _proxy = _connection.CreateHubProxy(hubName);
        }

        public async Task Connect()
        {
            await _connection.Start();
            var work = _proxy.Invoke("Register", "123456");
            _proxy.On("SendMessage", (string userId, string content) =>
            {
                OnReceiveEvent?.Invoke(userId, content);
            });
        }

        public Task Send(string username, string message)
        {
            var serverMethod = "Send";
            if (_connection.State != ConnectionState.Connected)
            {
                Debug.WriteLine("Not Connected");
                return null;
            }
            Debug.WriteLine("Connected");
            return _proxy.Invoke(serverMethod, username, message);
        }
    }
}