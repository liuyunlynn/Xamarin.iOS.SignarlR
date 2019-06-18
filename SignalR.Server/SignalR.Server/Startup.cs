using System;
using System.Timers;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using SignalR.Server.Hubs;

[assembly: OwinStartup(typeof(SignalR.Server.Startup))]

namespace SignalR.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            var timer = new Timer
            {
                Enabled = true,
                Interval = 1000
            };
            timer.Start();
            timer.Elapsed += SendMessage;
        }

        private void SendMessage(object sender, ElapsedEventArgs e)
        {
            if (SignalRUserInfo.Connections.ContainsKey("123456"))
            {
                GlobalHost.ConnectionManager.GetHubContext<ServerHub>().Clients
                    .Client(SignalRUserInfo.Connections["123456"]).SendMessage("123456", DateTime.Now.Second);
            }
        }
    }
}
