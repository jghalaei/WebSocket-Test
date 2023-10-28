using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace speed.Handlers
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;

        public WebSocketMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            IWebSocketHandler webSocketHandler = new WebSocketHandler();
            await webSocketHandler.HandleWebSocketAsync(context);
            await _next(context);
        }
    }
}