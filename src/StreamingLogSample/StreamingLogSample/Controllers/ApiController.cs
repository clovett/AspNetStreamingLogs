using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StreamingLogSample.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingLogSample.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        IHubContext<SignalRLog> _hubContext;

        public ApiController(IHubContext<SignalRLog> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("Ping")]
        [Authorize]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<string> Ping(string message)
        {
            var user = _hubContext.Clients.User(User.Identity.Name);
            if (user != null)
            {
                await user.SendAsync("broadcastMessage", message);
            }
            return "ok";
        }

        [HttpGet]
        [Route("Broadcast/{count}")]
        [Authorize]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<string> Broadcast(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await _hubContext.Clients.All.SendAsync("broadcastMessage", "broadcast " + i);
                await Task.Delay(100);
            }
            return "ok";
        }
    }
}
