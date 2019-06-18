using System.Collections.Generic;

namespace SignalR.Server.Hubs
{
    public class SignalRUserInfo
    {
        public static Dictionary<string, string> Connections = new Dictionary<string, string>();
    }
}