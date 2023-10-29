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
        [HttpGet("/")]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }
        [HttpGet("/messages")]
        public async Task GetSocket()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var clientId = Guid.NewGuid().ToString();
                WsSession wsSession = new WsSession(clientId, webSocket);

                var socketFinishedTask = new TaskCompletionSource<object>();
                BackgroundSocketProcessor.AddSession(wsSession, socketFinishedTask);

                await socketFinishedTask.Task;
            }
        }
        [HttpPost("/ping")]
        public async Task<IActionResult> Ping([FromHeader] string ClientId)
        {
            if (String.IsNullOrEmpty(ClientId))
                return BadRequest("ClientId is not valid");
            WsSession? wsSession = WsSession.GetSession(ClientId);
            if (wsSession == null)
                return NotFound("Connection not found");
            await wsSession.SendMessageAsync("Pong");
            return Ok();
        }
        [HttpPost("work/start")]
        public  IActionResult StartWork([FromHeader] string ClientId)
        {
            if (String.IsNullOrEmpty(ClientId))
                return BadRequest("ClientId is not valid");

            WsSession? wsSession = WsSession.GetSession(ClientId);
            if (wsSession == null)
                return NotFound("Connection not found");
            Task t = StartWorkAsync(wsSession);
            return Ok();
        }

        private async Task StartWorkAsync(WsSession wsSession)
        {
            int workId = new Random().Next(10000, 99999);
            await wsSession.SendMessageAsync("workStarted, ID: " + workId);
            await Task.Delay(TimeSpan.FromSeconds(new Random().Next(1, 5)));
            await wsSession.SendMessageAsync("workFinished, ID: " + workId);

        }
    }
}