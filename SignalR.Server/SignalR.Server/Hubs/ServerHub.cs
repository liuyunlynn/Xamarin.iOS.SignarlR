using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;

namespace SignalR.Server.Hubs
{
    public class ServerHub : Hub
    {
        public void Register(string userId)
        {
            if (SignalRUserInfo.Connections == null)
            {
                SignalRUserInfo.Connections = new Dictionary<string, string>();
            }

            if (!SignalRUserInfo.Connections.Keys.Contains(userId))
            {
                SignalRUserInfo.Connections.Add(userId, Context.ConnectionId);
            }
            else
            {
                SignalRUserInfo.Connections[userId] = Context.ConnectionId;
            }
        }

        public void Send(string userId, string content)
        {
            if (SignalRUserInfo.Connections.ContainsKey(userId))
            {
                Clients.Client(SignalRUserInfo.Connections[userId]).SendMessage(userId, content);
            }
        }
    }
}