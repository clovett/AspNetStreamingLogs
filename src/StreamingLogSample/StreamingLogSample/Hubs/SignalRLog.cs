using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace StreamingLogSample.Hubs
{
    [Authorize]
    public class SignalRLog : Hub
    {
        public SignalRLog()
        {
        }

        public Task BroadcastMessage(string message) =>
            Clients.All.SendAsync("broadcastMessage", message);
    }
}
