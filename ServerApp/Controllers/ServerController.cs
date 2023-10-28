using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using speed.Handlers;

namespace speed.Controllers
{
    [ApiController]
    public class ServerController : ControllerBase
    {
        private IWebSocketHandler _webSocketHandler;

        public ServerController(IWebSocketHandler webSocketHandler)
        {
            _webSocketHandler = webSocketHandler;
        }
        [HttpGet("/")]
        public async Task<IActionResult> Get()
        {
            return Ok("Hello World");
        }
        [HttpGet("/messages")]
        public async Task GetSocket()
        {
            await _webSocketHandler.HandleWebSocketAsync(HttpContext);
        }
        [HttpPost("/ping")]
        public async Task<IActionResult> Ping()
        {
            if (await _webSocketHandler.SendWebSocketMessageAsync(HttpContext.Session.Id, "Pong"))
                return Ok();
            return NotFound("Connection not found");
        }

        [HttpPost("work/start")]
        public async Task<IActionResult> StartWork()
        {
            try
            {
                Work work = new Work(HttpContext.Session.Id, _webSocketHandler);
                await work.StartAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}