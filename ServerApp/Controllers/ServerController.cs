using System.ComponentModel;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using ServerApp;
using ServerApp.Handlers;

namespace speed.Controllers
{
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly ILogger<WsSession> _logger;

        public ServerController(ILogger<WsSession> logger)
        {
            _logger = logger;
        }

        [HttpGet("/")]
        public IActionResult Get()
        {
            return Ok("Wellcome....");
        }
        [HttpGet("/messages")]
        public async Task AcceptSocket()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var clientId = Guid.NewGuid().ToString();

                WsSession wsSession = new WsSession(clientId, webSocket, _logger);
                _logger.LogInformation("Session Created for client {0}", clientId);
                await wsSession.StartAsync();
                _logger.LogInformation("Session Closed for client {0}", clientId);
            }
        }

        [HttpPost("/close")]
        public async Task<IActionResult> CloseSocket([FromHeader] string ClientId)
        {
            WsSession? wsSession = WsSession.GetSession(ClientId);
            if (wsSession == null)
                return NotFound("Connection not found");
            _logger.LogInformation("Disconnet requested for client {0}", ClientId);
            await wsSession.DisconnectAsync();
            return Ok();
        }

        [HttpPost("/ping")]
        public async Task<IActionResult> Ping([FromHeader] string ClientId)
        {
            if (String.IsNullOrEmpty(ClientId))
                return BadRequest("ClientId is not valid");
            WsSession? wsSession = WsSession.GetSession(ClientId);
            if (wsSession == null)
                return NotFound("Connection not found");
            _logger.LogInformation("Ping requested for client {0}", ClientId);
            await wsSession.SendMessageAsync("Pong");

            return Ok();
        }

        [HttpPost("work/start")]
        public IActionResult StartWork([FromHeader] string ClientId)
        {
            if (String.IsNullOrEmpty(ClientId))
                return BadRequest("ClientId is not valid");

            WsSession? wsSession = WsSession.GetSession(ClientId);
            if (wsSession == null)
                return NotFound("Connection not found");
            _logger.LogInformation("work/start requested for client {0}", ClientId);
            Task t = SimulateWorkAsync(wsSession);
            return Ok();
        }

        private async Task SimulateWorkAsync(WsSession wsSession)
        {
            int workId = new Random().Next(10000, 99999);
            await wsSession.SendMessageAsync("workStarted, ID: " + workId);
            await Task.Delay(TimeSpan.FromSeconds(new Random().Next(1, 5)));
            await wsSession.SendMessageAsync("workFinished, ID: " + workId);

        }
    }
}